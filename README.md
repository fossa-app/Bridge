# FossaApp Bridge
[![NuGet Version](https://img.shields.io/nuget/v/Fossa.Bridge.svg)](https://www.nuget.org/packages/Fossa.Bridge/)
[![npm Version](https://img.shields.io/npm/v/%40fossa-app%2Fbridge)](https://www.npmjs.com/package/@fossa-app/bridge)
[![Developed by Tigran (TIKSN) Torosyan](https://img.shields.io/badge/Developed%20by-%20Tigran%20%28TIKSN%29%20Torosyan-orange.svg)](https://tiksn.com/)
[![StandWithUkraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://github.com/vshymanskyy/StandWithUkraine/blob/main/docs/README.md)

The FossaApp Bridge provides shared frontend and backend functionality.

## Project Purpose
The Fossa.Bridge project is a shared F# library designed to encapsulate logic, validation, and domain models that are common to both the .NET backend and the TypeScript/JavaScript frontend. By using [Fable](https://fable.io/), the F# code is transpiled into JavaScript/TypeScript for the frontend environment while simultaneously being compiled into a standard .NET library for the backend. This ensures a single source of truth for core business logic, API models, and data validation rules, reducing duplication and preventing synchronization issues between the client and server.

## Package Usage

### For .NET Backends (C# / F#)
Install the NuGet package:

```bash
dotnet add package Fossa.Bridge
```

### For JavaScript / TypeScript Frontends
Install the NPM package:

```bash
npm install @fossa-app/bridge
```
or using Yarn:
```bash
yarn add @fossa-app/bridge
```

## Features
- Shared API domain models.
- Common validation and helper logic.
- Consistent behavior across client and server.

## Building and Packaging
To build both NPM and NuGet packages locally, run the included PowerShell script:

```bash
.\pack.ps1
```
