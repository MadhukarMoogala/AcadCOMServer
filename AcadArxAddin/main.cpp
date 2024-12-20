//////////////////////////////////////////////////////////////////////////////
//
//  Copyright 2024 Autodesk, Inc.  All rights reserved.
//
//  Use of this software is subject to the terms of the Autodesk license 
//  agreement provided at the time of installation or download, or which 
//  otherwise accompanies this software in either electronic or hard copy form.   
//
//////////////////////////////////////////////////////////////////////////////
// AsdkPlainComSamp.cpp
//
// This example shows how to use COM by itself (without MFC), to access
// the AutoCAD ActiveX Automation APIs.
//
#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#ifndef _ALLOW_RTCc_IN_STL
#define _ALLOW_RTCc_IN_STL
#endif // !defined(_ALLOW_RTCc_IN_STL)
#pragma warning( disable : 4278 )
#import "acax25ENU.tlb" no_implementation raw_interfaces_only named_guids
#pragma warning( default : 4278 )

#include <rxregsvc.h>
#include <aced.h>
#include <adslib.h>
#include "tchar.h"
#include <Contract_i.h>
#include "Contract_i.c"

void RunArxDll() {
    _COM_SMARTPTR_TYPEDEF(IServer, __uuidof(IServer));
    AutoCAD::IAcadApplication* pAcad = nullptr;
    HRESULT hr = NOERROR;
    LPDISPATCH pAcadDisp = acedGetIDispatch(true);

    if (pAcadDisp == NULL)
        return;

    hr = pAcadDisp->QueryInterface(AutoCAD::IID_IAcadApplication, (void**)&pAcad);
    pAcadDisp->Release();
    if (FAILED(hr) || pAcad == nullptr)
        return;

    IDispatchPtr pDisp = nullptr;
    hr = pAcad->GetInterfaceObject(_bstr_t("COMServer.Server"), &pDisp);
    pAcad->Release();  // Release the reference to avoid memory leak

    if (FAILED(hr) || pDisp == nullptr)
        return;

    IServerPtr pServer = nullptr;
    hr = pDisp->QueryInterface(IID_IServer, (void**)&pServer);
    if (FAILED(hr) || pServer == nullptr)
        return;

    double pi = 0.0;
    hr = pServer->ComputePi(&pi);
    if (SUCCEEDED(hr)) {
        acutPrintf(_T("\nValue of Pi is %f"), pi);
    }
    else {
        acutPrintf(_T("\nError computing Pi: %08X"), hr);
    }
}

void
initApp()
{
    acedRegCmds->addCommand(_T("AsdkComAccess_COMMANDS"), _T("AsdkRunArx"), _T("RunArx"), ACRX_CMD_MODAL, RunArxDll);
}


void unloadApp()
{
    
    
    // Remove the command group added via acedRegCmds->addCommand
    //
    acedRegCmds->removeGroup(_T("AsdkComAccess_COMMANDS"));

}

extern "C" AcRx::AppRetCode 
acrxEntryPoint(AcRx::AppMsgCode msg, void* appId)
{
    switch (msg) {
    case AcRx::kInitAppMsg:
        acrxDynamicLinker->unlockApplication(appId);
        acrxDynamicLinker->registerAppMDIAware(appId);
        initApp();
        break;
    case AcRx::kUnloadAppMsg:
        unloadApp();
        break;
    case AcRx::kLoadDwgMsg:

        break;
    case AcRx::kUnloadDwgMsg:

        break;
    case AcRx::kInvkSubrMsg:

        break;
    default:
        ;
    }
    return AcRx::kRetOK;
}

