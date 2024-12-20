## Bridging .NET Core and AutoCAD: Exposing and Using COM Server Components with GetInterfaceObject

### Overview

This guide demonstrates how to expose COM server components in .NET Core and utilize them in AutoCAD using `GetInterfaceObject`. This workflow is suitable for AutoCAD 2025 and later versions.

---

### Why Use COM in .NET Core?

COM (Component Object Model) provides a standardized way for software components to communicate and has been a popular technology for extending AutoCAD for many years. While .NET Framework had seamless support for COM, achieving this in .NET Core or .NET 5+ requires additional effort but enables the use of modern frameworks like .NET 8.0.

### Key Features

In this example, we’ll create a COM server to calculate Pi and use it in AutoCAD.  
Both .NET 8 and ARX add-in projects are provided.

We will register the COM server created in .NET 8.0 and consume it in both .NET Core and ARX AutoCAD plugins.

To consume the server, we will use the `GetInterfaceObject` AutoCAD ActiveX API.

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
   cd ComServer
   dotnet restore
   dotnet build
   ```

3. Register the COM server:
   
   ```bash
   regsvr32 ~AcadCOMServer\Binaries\net8.0\COMServer.comhost.dll
   ```

4. Build the AutoCAD .NET plugin
   
   ```bash
   cd AcadNetAddin
   dotnet build
   ```

5. Build the AutoCAD Arx plugin
   
   ```bash
   cd AcadArxAddin
   msbuild /t:build /p:Configuration=Debug;Platform=x64
   ```
- Launch AutoCAD 2025 and load the plugin:
  
  - Use the `NETLOAD` command to load the .NET app.
    
        `~AcadComServer\Binaries\net8.0\AcadNetAddin.dll`
  
  - Use the `Appload` command to load the arx app.
    
        `~AcadComServer\Binaries\net8.0\AcadArxAddin.dll`

- Run the `RunDotnet` command:
  
  - A calculated value of Pi will be written to the AutoCAD command line.

- Run the `RunArx` command :
  
  - A calculated value of Pi will be written to the AutoCAD command line.

#### Clone and Build Through Visual Studio UI

To get started with the repository and build the solution, follow these steps:

1. **Launch Visual Studio with Administrative Privileges**
   
   - Ensure Visual Studio is run as an administrator to handle any system-level dependencies during the build process.

2. **Clone the Repository**
   
   - On the Visual Studio start page, select **Clone a Repository**.
   - Enter the following GitHub URL in the repository location field:  
     `https://github.com/MadhukarMoogala/AcadCOMServer.git`
   - Choose the desired folder path where you want to clone the repository, and click **Clone**.

3. **Set Up the ObjectARX SDK Path**
   
   - After the cloning process, navigate to the `AcadArxAddin` project within the solution.
   - Right-click on the `AcadArxAddin` project in **Solution Explorer**, select **Properties**, and set the path to your installed **ObjectARX SDK**.
     - For example, update the **Include Directories** and **Library Directories** paths in the project settings.
   - Ensure that the ObjectARX SDK is correctly installed and accessible.

4. **Restore NuGet Packages**
   
   - Visual Studio will automatically detect and restore any missing NuGet packages upon opening the solution. If needed, you can manually restore packages by right-clicking the solution in **Solution Explorer** and selecting **Restore NuGet Packages**.

5. **Build the Solution**
   
   - Right-click on the solution in **Solution Explorer** and select **Build Solution**.
   - Verify that all projects build successfully without errors.

6. **Run and Test**
   
   - Once the build is complete, the `COM Server`, the `AcadArxAddin`  and the `AcadNetAddin` will be ready to use. 
   
   - Don't forget `regsiter the COM Server` 
   
   - Launch AutoCAD 2025 and load the plugin:
     
     - Use the `NETLOAD` command to load the .NET app.
       
           `~AcadComServer\Binaries\net8.0\AcadNetAddin.dll`
     
     - Use the `Appload` command to load the arx app.
       
           `~AcadComServer\Binaries\net8.0\AcadArxAddin.dll`
   
   - Run the `RunDotnet` command:
     
     - A calculated value of Pi will be written to the AutoCAD command line.
   
   - Run the `RunArx` command :
     
     - A calculated value of Pi will be written to the AutoCAD command line.

### Written

Madhukar Moogala *APS  Developer Advocate*
