// SPDX-FileCopyrightText: 2023 Debug <49997488+DebugOk@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Moony <moony@hellomouse.net>
// SPDX-FileCopyrightText: 2024 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 sleepyyapril <123355664+sleepyyapril@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Content.Server.Administration;
using Content.Server.Administration.Managers;
using Content.Shared.Administration;
using Robust.Shared.Player;
using Robust.Shared.Toolshed;
using Robust.Shared.Toolshed.Syntax;

namespace Content.Server.Toolshed.Commands.AdminDebug;

[ToolshedCommand, AdminCommand(AdminFlags.Debug)]
public sealed class ACmdCommand : ToolshedCommand
{
    [Dependency] private readonly IAdminManager _adminManager = default!;

    [CommandImplementation("perms")]
    public AdminFlags[]? Perms([PipedArgument] CommandSpec command)
    {
        var res = _adminManager.TryGetCommandFlags(command, out var flags);
        if (res)
            flags ??= Array.Empty<AdminFlags>();
        return flags;
    }

    [CommandImplementation("caninvoke")]
    public bool CanInvoke(IInvocationContext ctx, [PipedArgument] CommandSpec command, ICommonSession player)
    {
        // Deliberately discard the error.
        return ((IPermissionController) _adminManager).CheckInvokable(command, player, out _);
    }
}
