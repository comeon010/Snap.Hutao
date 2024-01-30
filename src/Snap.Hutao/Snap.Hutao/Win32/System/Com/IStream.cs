﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Snap.Hutao.Win32.Foundation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Snap.Hutao.Win32.System.Com;

[SupportedOSPlatform("windows5.0")]
[Guid("0000000C-0000-0000-C000-000000000046")]
internal unsafe struct IStream
{
    public readonly Vftbl* ThisPtr;

    internal static unsafe ref readonly Guid IID
    {
        get
        {
            ReadOnlySpan<byte> data = [0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x46];
            return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
        }
    }

    public unsafe HRESULT QueryInterface<TInterface>(ref readonly Guid riid, out TInterface* pvObject)
        where TInterface : unmanaged
    {
        fixed (Guid* riid2 = &riid)
        {
            fixed (TInterface** ppvObject = &pvObject)
            {
                return ThisPtr->ISequentialStreamVftbl.IUnknownVftbl.QueryInterface((IUnknown*)Unsafe.AsPointer(ref this), riid2, (void**)ppvObject);
            }
        }
    }

    public uint AddRef()
    {
        return ThisPtr->ISequentialStreamVftbl.IUnknownVftbl.AddRef((IUnknown*)Unsafe.AsPointer(ref this));
    }

    public uint Release()
    {
        return ThisPtr->ISequentialStreamVftbl.IUnknownVftbl.Release((IUnknown*)Unsafe.AsPointer(ref this));
    }

    internal readonly struct Vftbl
    {
        internal readonly ISequentialStream.Vftbl ISequentialStreamVftbl;
        internal readonly delegate* unmanaged[Stdcall]<IStream*, long, STREAM_SEEK, ulong*, HRESULT> Seek;
        internal readonly delegate* unmanaged[Stdcall]<IStream*, ulong, HRESULT> SetSize;
        internal readonly delegate* unmanaged[Stdcall]<IStream*, IStream*, ulong, ulong*, ulong*, HRESULT> CopyTo;
        internal readonly delegate* unmanaged[Stdcall]<IStream*, uint, HRESULT> Commit;
        internal readonly delegate* unmanaged[Stdcall]<IStream*, HRESULT> Revert;
        internal readonly delegate* unmanaged[Stdcall]<IStream*, ulong, ulong, uint, HRESULT> LockRegion;
        internal readonly delegate* unmanaged[Stdcall]<IStream*, ulong, ulong, uint, HRESULT> UnlockRegion;
        internal readonly delegate* unmanaged[Stdcall]<IStream*, STATSTG*, uint, HRESULT> Stat;
        internal readonly delegate* unmanaged[Stdcall]<IStream*, IStream**, HRESULT> Clone;
    }
}