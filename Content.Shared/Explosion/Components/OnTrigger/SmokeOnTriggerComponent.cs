// SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 deltanedas <@deltanedas:kde.org>
// SPDX-FileCopyrightText: 2024 Aexxie <codyfox.077@gmail.com>
// SPDX-FileCopyrightText: 2024 Magnus Larsen <i.am.larsenml@gmail.com>
// SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
// SPDX-FileCopyrightText: 2024 sleepyyapril <flyingkarii@gmail.com>
// SPDX-FileCopyrightText: 2025 sleepyyapril <123355664+sleepyyapril@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Content.Shared.Explosion.EntitySystems;
using Content.Shared.Chemistry.Components;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared.Explosion.Components;

/// <summary>
/// Creates a smoke cloud when triggered, with an optional solution to include in it.
/// No sound is played incase a grenade is stealthy, use <see cref="SoundOnTriggerComponent"/> if you want a sound.
/// </summary>
[RegisterComponent, NetworkedComponent, Access(typeof(SharedSmokeOnTriggerSystem))]
public sealed partial class SmokeOnTriggerComponent : Component
{
    /// <summary>
    /// How long the smoke stays for, after it has spread.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public float Duration = 10;

    /// <summary>
    /// How much the smoke will spread.
    /// </summary>
    [DataField(required: true), ViewVariables(VVAccess.ReadWrite)]
    public int SpreadAmount;

    /// <summary>
    /// Smoke entity to spawn.
    /// Defaults to smoke but you can use foam if you want.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public EntProtoId SmokePrototype = "Smoke";

    /// <summary>
    /// Solution to add to each smoke cloud.
    /// </summary>
    /// <remarks>
    /// When using repeating trigger this essentially gets multiplied so dont do anything crazy like omnizine or lexorin.
    /// </remarks>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public Solution Solution = new();
}
