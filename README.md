# NWebDAV
.NET implementation of a WebDAV server.

## Overview
I needed a WebDAV server implementation for C#, but I couldn't find an implementation that fulfilled my needs. That's why I wrote my own.

### Requirements

* Fast, scalable, robust with moderate memory usage.
* Abstract data store, so it can be used for directories/files but also for any other data.
* Abstract locking providers, so it can use in-memory locking (small servers) or Redis (large installations).
* Flexible and extensible property management.
* Compatible with ASP.NET Core (previous version worked with .NET Framework too)
* Fully supports .NET framework, Mono and the Core CLR.
* Allows for various HTTP authentication methods (including basic authentication).

# Building the samples
The repository comes with various samples that use NWebDAV:

 * `NWebDav.Sample.Simple` implements a WebDAV server that doesn't use any authentication and maps to a local drive.
 * `NWebDav.Sample.BasicAuth` implements a WebDAV server that uses Basic authentication and maps to a local drive. Note that most WebDAV clients require an HTTPS connection, so this example registers an HTTPS endpoint too (make sure you register the self-signed certificate).
 * `NWebDav.Sample.AzureBlob` implements a WabDAV server that maps to an Azure Storage Account and allows access to all the BLOB containers in the storage account. This example illustrates the use of different collection types and how the server can be used with custom stores.
  
   **IMPORTANT**: The Azure BLOB store hasn't been fully tested and shouldn't be used for production without extensive testing. Feel free to submit PRs if you encounter any issues.

## Register the certificate
Examples that use HTTPS use a certificate in `Certificates/localhost.pfx`. This certificate can be generated by running `GenerateCert.ps1` (requires elevated privilege to register the certificate in the computer's Trusted Root Authorities). The certificate is protected using a password (`nwebdav`).

## NWebDav.Sample.Simple

## WebDAV client on Windows Vista/7
The Windows Vista/7 WebDAV client is implemented poorly. We have support for this client since the 0.1.7 release.

* It required the 'D' namespace prefix on all DAV related XML nodes. XML namespaces without prefixes are not supported.
* It cannot deal with XML date time format (ISO 8601) in a decent manner. It processes the fraction part as milliseconds, which is wrong. Milliseconds can be between 0 and 999, where a fraction can have more than 3 digits. The difference is subtle. __2016-04-14T01:02:03.12__ denotes 120ms, but it could be parsed as 12ms by Windows 7 clients. __2016-04-14T01:02:03.1234__ denotes 123.4ms, but cannot be parsed when using integers. Windows 7 clients don't accept this format. For that reason we will not output more than 3 digits for the fraction part.

Windows 7 client might perform very bad when connecting to any WebDAV server (not related to this specific implementation). This is caused, because it tries to auto-detect any proxy server before __any__ request. Refer to [KB2445570](https://support.microsoft.com/en-us/kb/2445570) for more information.

## Work in progress
This module is currently work-in-progress and shouldn't be used for production use yet. If you want to help, then let me know... The following features are currently missing:

* Only the in-memory locking provider has been implemented yet.
* Check if each call responds with the proper status codes (as defined in the WebDAV specification).
* Recursive locking is not supported yet.
* We should have compatibility flags that can be used to implement quirks for bad WebDAV clients. We can detect the client based on the User-Agent and provide support for it.

The current version seems to work fine to serve files using WebDAV on both Windows and OS X.

# Contact
If you have any questions and/or problems, then you can submit an issue. For other remarks, you can also contact me via email at <mail@ramondeklein.nl>.

# Donate
I never intended to make any profit for this code, but I received a request to add a donation link. So if you think that you want to donate to this project, then you can use the following button to donate to me via PayPal (don't feel obliged to do so).

[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=KZYDXR3ERJQZJ)
