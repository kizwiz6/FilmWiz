# FilmWiz - Film Search Application

## Overview
FilmWiz is a modern web application built using Blazor WebAssembly that allows users to search for films using the OMDb API. The application features a clean, responsive interface built with MudBlazor components and follows clean architecture principles.

![FilmWiz Home Screen](https://i.imgur.com/IgTBpnB.png)

## Features
- Search films by title
- Real-time search feedback with loading states
- Responsive design for mobile and desktop
- Error handling with user-friendly messages
- Clean and intuitive Material Design interface

## Technologies Used
- .NET 9.0
- Blazor WebAssembly
- MudBlazor Component Library
- OMDb API
- xUnit for Testing
- Moq for Mocking

## Project Structure
The solution follows Clean Architecture principles and is organized into the following projects:
- **FilmWiz.Core**: Contains domain models and interfaces
- **FilmWiz.Infrastructure**: Handles external service integration (OMDb API)
- **FilmWiz.Web**: Blazor WebAssembly application
- **FilmWiz.Tests**: Unit tests

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- An OMDb API key (obtain from [https://www.omdbapi.com/](https://www.omdbapi.com/))

### Installation
1. Clone the repository
```bash
git clone https://github.com/yourusername/FilmWiz.git
