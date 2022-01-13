
![checklist_logo](https://github.com/emmysteven/checklist/blob/main/media/checklist.png)

<div align="center">
	<img alt="status" src="https://img.shields.io/badge/Status-developing-brightgree">
	<img alt="GitHub Workflow Status" src="https://img.shields.io/github/workflow/status/emmysteven/checklist/.NET">
	<img alt="GitHub repo size" src="https://img.shields.io/github/repo-size/emmysteven/checklist">
	<img alt="GitHub closed issues" src="https://img.shields.io/github/issues-closed/emmysteven/checklist">
	<img alt="GitHub stars" src="https://img.shields.io/github/stars/emmysteven/checklist">
	<img alt="visitors" src="https://visitor-badge.laobi.icu/badge?page_id=checklist">
	<a href="https://twitter.com/intent/tweet?hashtags=checklist,foodie,dotnet,oss&text=🥘+Restuarant.+A+booking+platform+for+those+who+love+eating+outdoors&url=https%3A%2F%2Fgithub.com%2Femmysteven%2Fchecklist&via=emmysteven_">
		<img alt="Twitter" src="https://img.shields.io/twitter/url/http/shields.io.svg?style=flat&logo=twitter">
	</a>


</div>

# Checklist
<br/>

An Implementation of Clean Architecture with .NET 6
With this Open-Source Codebase, you will get access to the world of Loosely-Coupled and Inverted-Dependency Architecture in .NET 6

## Upcoming Release

### v1.1-release

Read the [Changelog file](https://github.com/emmysteven/checklist/blob/main/CHANGELOG.md) to see the new changes.

Clone this repository to get the latest unreleased version.


## Releases

v1.0-preview - IS NOT YET OUT<!--[Download the first Preview here](https://github.com/emmysteven/checklist/releases/tag/v1.0-preview) -->

### v1 Preview.
Follow these steps to get started
1. Clone this Repository and Extract it to a Folder.
2. Change appsettings.mock.json to appsettings.json in WebUI directory
3. After following step 2, change all <code>ChangeMe</code> to your own value
- [Create an ethereal mail account](https://ethereal.email/)
- Use your credentials to fill up the EmailSettings section in appsettings.json
- Install redis on your local machine
4. Run <code>npm install</code> in the src/WebUI/ClientApp directory
5. Run the following commands on Terminal in the WebUI project's directory.
- dotnet restore
- dotnet ef database update --context DataContext
- dotnet run (OR) Run the Solution using Visual Studio 2019 or JetBrain Rider

Check out my [blog](https://www.mycodegist.com) or say [Hi on Twitter!](https://twitter.com/emmysteven_)

## Purpose of this Project

The purpose of this project is to provide an e-checklist for End-of-Day operations on Oracle Flexcube without having to use a paper checklist.

This project is well documented along with the steps taken to build this solution from scratch. You can tweak it to suite your taste.
- Demonstrate clean architecture in .NET 6
- This is not a proof of concept
- Implementation that is ready for production
- Integrate the most essential libraries and packages



## Technologies
- .NET 6
- REST Standards
- .NET 5 Libraries



## Features
- [x] Clean Architecture
- [x] CQRS with MediatR Library
- [x] Entity Framework Core - Code First
- [x] Repository Pattern - Generic
- [x] MediatR Pipeline Logging & Validation
- [x] Serilog
- [ ] Swagger UI
- [x] Response Wrappers
- [x] Healthchecks
- [x] Pagination
- [ ] In-Memory Caching
- [x] Redis Caching
- [x] RDBMS
- [x] JWT Authentication
- [x] Role based Authorization
- [x] Custom Exception Handler
- [x] Fluent Validation
- [x] Automapper
- [x] SMTP / Mailkit / Email Service
- [ ] Complete User Management Module (Register / Generate Token / Forgot Password / Confirmation Mail)
- [x] User Auditing



## Prerequisites
- JetBrains Rider 2021.3.2 & above | Visual Studio 2019 Community and above
- .NET Core 6.0 SDK and above
- Basic Understanding of Clean Architecture
- I Recommend that you read [Onion Architecture In ASP.NET Core With CQRS – Detailed](https://www.codewithmukesh.com/blog/onion-architecture-in-aspnet-core/) article to understand this project much better. This project is just an Advanced Version of the mentioned article.

## Getting Started

## Changelog
Every changes / additions / deletions will be recorded in the [Changelog file](https://github.com/emmysteven/Restaurant/blob/main/CHANGELOG.md).

## Questions? Bugs? Suggestions for Improvement?
Having any issues or troubles getting started? [Get in touch with me](https://www.mycodegist.com/contact) or [Raise a Bug or Feature Request](https://github.com/emmysteven/restaurant/issues/new/choose). Always happy to help.

## Support
If this project helped you learn something new? or helped you at work? Do Consider Supporting. <br/>Give it a star <br/>ETH: <code>0x9F4942911f2406E5897669Db99184d47B3078E99</code>


## Share it!
There are many improvements and fixes along the way from the day I started out. Thanks to the community for the support and suggestions.
Please share this Repository within your developer community, if you think this would make a difference! Thanks.

## About the Author
### Emmy Steven
- Blogs at [mycodegist.com](https://www.mycodegist.com)
- Twitter - [Emmy Steven](https://www.twitter.com/emmysteven_)
- Linkedin - [Emmy Steven](https://www.linkedin.com/in/emmysteven/)

## Licensing
emmysteven/restaurant Project is licensed with the [MIT License](https://github.com/emmysteven/checklist/blob/main/LICENSE).
