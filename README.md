# 2pdf.dotnet
___
## Description

.NET SDK for [2pdf.io](https://2pdf.io) For extremely fast HTML/URL to PDF/Image conversion, capable of processing thousands of files per day.

## Requirements

- .NET 6.0, .NETStandart 2.0 
- Visual Studio 2019 and higher

## Installation
For installing the client run this command `Install-Package 2Pdf -Version 1.0.2`

## Get Api Key
To get your Api Key you should to visit [Web Site](https://2pdf.io) and tap `GET API KEY` button.

## Usage

Below is a simple examples of how to convert the HTML/URL to PDF/Image:
####
#### Convert the HTML to PDF ([Documentation](https://2pdf.io/Home#postPdfFromHtmlId))
```csharp
var converter = new HtmlToPdf("{YOUR_API_KEY}"); // Initialize the Client
var document = await converter.SendAsync("<p>Html to PDF converter</p>");
document.SaveToFile("path_to_file.pdf"); // (Optioanl) save byte array into the file
```
####
#### Convert the URL to PDF ([Documentation](https://2pdf.io/Home#postPdfFromUrlId))
```csharp
var converter = new UrlToPdf("{YOUR_API_KEY}"); // Initialize the Client

var document = await converter
    .AddParameter(ParameterPdfKey.Grayscale, null) // Add some parameters
    .AddParameter(ParameterPdfKey.Orientation, "Album")
    .SendAsync("google.com");

document.SaveToFile("path_to_file.pdf"); // (Optioanl) save byte array into the file
```
####
#### Convert the HTML to Image ([Documentation](https://2pdf.io/Home#postImageFromHtmlId))
```csharp
var converter = new HtmlToImage("{YOUR_API_KEY}"); // Initialize the Client

var document = await converter
    .AddParameter(ParameterImageKey.Height, 500) // Add some parameters
    .SendAsync("<p>Html to Image converter</p>");

document.SaveToFile("path_to_file.png"); // (Optioanl) save byte array into the file
```
####
#### Convert the URL to Image ([Documentation](https://2pdf.io/Home#postImageFromUrlId))
```csharp
var converter = new UrlToPdf("{YOUR_API_KEY}"); // Initialize the Client

var document = await converter
    .AddParameters(new Dictionary<ParameterImageKey, object>
    {
        { ParameterImageKey.Height, 500 }, // Add some parameters
        { ParameterImageKey.Width, 200 },
    })
    .SendAsync("google.com");

document.SaveToFile("path_to_file.png"); // (Optioanl) save byte array into the file
```
