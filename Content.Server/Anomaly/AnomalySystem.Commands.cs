// SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 sleepyyapril <123355664+sleepyyapril@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Content.Server.Administration;
using Content.Shared.Administration;
using Content.Shared.Anomaly.Components;
using Robust.Shared.Console;

namespace Content.Server.Anomaly;

public sealed partial class AnomalySystem
{
    [Dependency] private readonly IConsoleHost _consoleHost = default!;

    public void InitializeCommands()
    {
        _consoleHost.RegisterCommand("pulseanomaly", Loc.GetString("anomaly-command-pulse"), "pulseanomaly <uid>",
            PulseAnomalyCommand,
            GetAnomalyCompletion);

        _consoleHost.RegisterCommand("supercriticalanomaly", Loc.GetString("anomaly-command-supercritical"), "supercriticalanomaly <uid>",
            SupercriticalAnomalyCommand,
            GetAnomalyCompletion);
    }

    [AdminCommand(AdminFlags.Fun)]
    private void PulseAnomalyCommand(IConsoleShell shell, string argstr, string[] args)
    {
        if (args.Length != 1)
            shell.WriteError("Argument length must be 1");

        if (!NetEntity.TryParse(args[0], out var uidNet) || !TryGetEntity(uidNet, out var uid))
            return;

        if (!TryComp<AnomalyComponent>(uid, out var anomaly))
            return;

        DoAnomalyPulse(uid.Value, anomaly);
    }

    [AdminCommand(AdminFlags.Fun)]
    private void SupercriticalAnomalyCommand(IConsoleShell shell, string argstr, string[] args)
    {
        if (args.Length != 1)
            shell.WriteError("Argument length must be 1");

        if (!NetEntity.TryParse(args[0], out var uidNet) || !TryGetEntity(uidNet, out var uid))
            return;

        if (!HasComp<AnomalyComponent>(uid))
            return;

        StartSupercriticalEvent(uid.Value);
    }

    private CompletionResult GetAnomalyCompletion(IConsoleShell shell, string[] args)
    {
        return args.Length != 1
            ? CompletionResult.Empty
            : CompletionResult.FromHintOptions(CompletionHelper.Components<AnomalyComponent>(args[0]), "<uid>");
    }
}
