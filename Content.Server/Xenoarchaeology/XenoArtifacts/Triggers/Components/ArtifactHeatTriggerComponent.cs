// SPDX-FileCopyrightText: 2022 Alex Evgrashin <aevgrashin@yandex.ru>
// SPDX-FileCopyrightText: 2022 mirrorcult <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 sleepyyapril <123355664+sleepyyapril@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

namespace Content.Server.Xenoarchaeology.XenoArtifacts.Triggers.Components;

// TODO: This should probably be generalized for cold temperature too,
// but right now there is no sane way to make a freezer.

/// <summary>
///     Triggers artifact if its in hot environment or
///     has contacted with a hot object (lit welder, lighter, etc).
/// </summary>
[RegisterComponent]
public sealed partial class ArtifactHeatTriggerComponent : Component
{
    /// <summary>
    ///     Minimal surrounding gas temperature to trigger artifact.
    ///     Around 100 degrees celsius by default.
    ///     Doesn't affect hot items temperature.
    /// </summary>
    [DataField("activationTemperature")]
    [ViewVariables(VVAccess.ReadWrite)]
    public float ActivationTemperature = 373;

    /// <summary>
    ///     Should artifact be activated by hot items (welders, lighter, etc)?
    /// </summary>
    [DataField("activateHot")]
    [ViewVariables(VVAccess.ReadWrite)]
    public bool ActivateHotItems = true;
}
