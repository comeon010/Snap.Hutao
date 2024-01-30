﻿// Copyright (c) DGP Studio. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Windows.AppLifecycle;

namespace Snap.Hutao.Core.LifeCycle;

internal sealed class HutaoActivationArguments
{
    public bool IsRedirectTo { get; set; }

    public HutaoActivationKind Kind { get; set; }

    public Uri? ProtocolActivatedUri { get; set; }

    public string? LaunchActivatedArguments { get; set; }

    public static HutaoActivationArguments FromAppActivationArguments(AppActivationArguments args, bool isRedirected = false)
    {
        HutaoActivationArguments result = new()
        {
            IsRedirectTo = isRedirected,
        };

        switch (args.Kind)
        {
            case ExtendedActivationKind.Launch:
                {
                    result.Kind = HutaoActivationKind.Launch;
                    if (args.TryGetLaunchActivatedArguments(out string? arguments))
                    {
                        result.LaunchActivatedArguments = arguments;
                    }

                    break;
                }

            case ExtendedActivationKind.Protocol:
                {
                    result.Kind = HutaoActivationKind.Protocol;
                    if (args.TryGetProtocolActivatedUri(out Uri? uri))
                    {
                        result.ProtocolActivatedUri = uri;
                    }

                    break;
                }
        }

        return result;
    }
}