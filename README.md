## Bridging .NET Core and AutoCAD: Exposing and Using COM Server Components with GetInterfaceObject

### Overview

This guide demonstrates how to expose COM server components in .NET Core and utilize them in AutoCAD using `GetInterfaceObject`. This workflow is suitable for AutoCAD 2025 and later versions.

---

### Why Use COM in .NET Core?

COM (Component Object Model) provides a standardized way for software components to communicate and has been a popular technology for extending AutoCAD for many years. While .NET Framework had seamless support for COM, achieving this in .NET Core or .NET 5+ requires additional effort but enables the use of modern frameworks like .NET 8.0.

In this example, we’ll create a COM server to calculate Pi and use it in AutoCAD.

---

### Prerequisites

Ensure the following tools and environments are set up before proceeding:

- **AutoCAD 2025 or later**
- **Visual Studio 2022 (v17.10 or later)**
- **.NET 8.0 SDK**

---

### Build and Run

Follow these steps to clone, build, and run the example:

1. Clone the repository:
   
   ```bash
   git clone https://github.com/MadhukarMoogala/AcadCOMServer.git
   cd AcadCOMServer
   ```

2. Restore dependencies and build the project:
   
   ```bash
   dotnet restore
   dotnet build
   ```

3. Register the COM server:
   
   ```bash
   regsvr32 ~AcadCOMServer\Binaries\net8.0\COMServer.comhost.dll
   ```

4. Launch AutoCAD 2025 and load the plugin:
   
   - Use the `NETLOAD` command to load the plugin:
     
     ```
     ~AcadCOMServer\Binaries\net8.0\AcadAddin.dll
     ```

5. Run the `RunDLL` command:
   
   - A calculated value of Pi will be written to the AutoCAD command line.

---

### Written

Madhukar Moogala *APS  Developer Advocate*
