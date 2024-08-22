# --------------------------------------------------------------------------
# Script Name: GenerateSSISPackage.ps1
# Description: This script automates the creation of an SSIS package that 
#              retrieves a JWT access token from an authentication endpoint 
#              and uses that token to fetch data from another API. The data 
#              is then saved to a text file. The script creates the SSIS 
#              package programmatically and saves it as a .dtsx file.
# 
# --------------------------------------------------------------------------

# --------------------------------------------------------------------------
# Requirements:
# 1. SQL Server Data Tools (SSDT) installed on the machine where the script is executed.
#    - SSDT is required to create and edit SSIS packages.
#    - Ensure that the correct version of SSDT is installed based on your SQL Server version.
# 2. SQL Server Integration Services (SSIS) runtime must be installed.
#    - SSIS runtime is necessary for running and managing SSIS packages.
# 3. .NET Framework is required for running PowerShell and managing assemblies.
#    - Ensure that .NET Framework is installed and up to date.
# 4. PowerShell version 5.0 or later is recommended.
#    - This script uses features available in newer versions of PowerShell.
# 5. Internet access to reach the API endpoints for authentication and data retrieval.
#    - Ensure the machine has the necessary permissions to access external APIs.
# 6. Newtonsoft.Json library should be available for JSON processing in the Script Task.
#    - This library is required for handling JSON responses from the API.
#    - Install it via NuGet if not already available.
# 7. The script assumes that the user has the necessary API credentials (username and password).
#    - Replace placeholders with actual credentials in the script.
# --------------------------------------------------------------------------

# Load the required assemblies to work with SSIS packages
Add-Type -AssemblyName "Microsoft.SqlServer.ManagedDTS, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"

# Initialize a new SSIS package object
$package = New-Object Microsoft.SqlServer.Dts.Runtime.Package

# Add necessary variables to the SSIS package
# These variables will store the URLs, credentials, JWT token, and the output file path

$package.Variables.Add("AuthUrl", $false, $false, "http://localhost:8080/api/auth/login") # URL for authentication API
$package.Variables.Add("DataUrl", $false, $false, "http://localhost/api/attorneys")       # URL for data retrieval API
$package.Variables.Add("Username", $false, $false, "your_username")                       # API Username (Replace with actual username)
$package.Variables.Add("Password", $false, $false, "your_password")                       # API Password (Replace with actual password)
$package.Variables.Add("JwtToken", $false, $false, "")                                    # Variable to store the JWT token
$package.Variables.Add("OutputFilePath", $false, $false, "C:\path\to\output\attorneys.txt") # File path to save the retrieved data

# Create a Script Task to retrieve the JWT token
# This task makes an HTTP POST request to the authentication endpoint and retrieves the token

$task1 = $package.Executables.Add("STOCK:ScriptTask")
$task1Host = [Microsoft.SqlServer.Dts.Runtime.TaskHost]$task1
$task1Host.Name = "GetJwtToken"
$task1Host.Properties["ScriptLanguage"].Value = "CSharp"

$task2Code = @"
using System;
using System.Net.Http;
using System.IO;
using Microsoft.SqlServer.Dts.Runtime;

public void Main()
{
    // Retrieve SSIS variables
    string dataUrl = Dts.Variables["User::DataUrl"].Value.ToString();
    string jwtToken = Dts.Variables["User::JwtToken"].Value.ToString();
    string outputFilePath = Dts.Variables["User::OutputFilePath"].Value.ToString();

    // Perform HTTP GET request to retrieve data using the JWT token
    using (HttpClient client = new HttpClient())
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

        HttpResponseMessage response = client.GetAsync(dataUrl).Result;
        if (response.IsSuccessStatusCode)
        {
            // Write the response content to the specified text file
            string result = response.Content.ReadAsStringAsync().Result;
            File.WriteAllText(outputFilePath, result); // This line saves the data to a .txt file
        }
        else
        {
            // If the request fails, set the task result to failure and exit
            Dts.TaskResult = (int)ScriptResults.Failure;
            return;
        }
    }

    Dts.TaskResult = (int)ScriptResults.Success;
}
"@

$task1Host.Properties["ScriptCode"].Value = $task1Code

# Create a Script Task to retrieve data using the JWT token
# This task makes an HTTP GET request to the data retrieval endpoint using the token and writes the response to a text file

$task2 = $package.Executables.Add("STOCK:ScriptTask")
$task2Host = [Microsoft.SqlServer.Dts.Runtime.TaskHost]$task2
$task2Host.Name = "GetDataAndWriteToFile"
$task2Host.Properties["ScriptLanguage"].Value = "CSharp"

$task2Code = @"
using System;
using System.Net.Http;
using System.IO;
using Microsoft.SqlServer.Dts.Runtime;

public void Main()
{
    // Retrieve SSIS variables
    string dataUrl = Dts.Variables["User::DataUrl"].Value.ToString();
    string jwtToken = Dts.Variables["User::JwtToken"].Value.ToString();
    string outputFilePath = Dts.Variables["User::OutputFilePath"].Value.ToString();

    // Perform HTTP GET request to retrieve data using the JWT token
    using (HttpClient client = new HttpClient())
    {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

        HttpResponseMessage response = client.GetAsync(dataUrl).Result;
        if (response.IsSuccessStatusCode)
        {
            // Write the response content to the specified text file
            string result = response.Content.ReadAsStringAsync().Result;
            File.WriteAllText(outputFilePath, result);
        }
        else
        {
            // If the request fails, set the task result to failure and exit
            Dts.TaskResult = (int)ScriptResults.Failure;
            return;
        }
    }

    Dts.TaskResult = (int)ScriptResults.Success;
}
"@

$task2Host.Properties["ScriptCode"].Value = $task2Code

# Add a precedence constraint to ensure the data retrieval task only runs if the token retrieval task succeeds
$package.PrecedenceConstraints.Add($task1Host, $task2Host)

# Save the generated SSIS package to a .dtsx file
# Specify the path where you want to save the package
$application = New-Object Microsoft.SqlServer.Dts.Runtime.Application
$application.SaveToXml("C:\path\to\output\MySSISPackage.dtsx", $package, $null) # Replace with your desired path

Write-Host "SSIS package generated successfully."

