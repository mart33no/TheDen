// SPDX-FileCopyrightText: 2022 Moony <moonheart08@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 wrexbe <81056464+wrexbe@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 TemporalOroboros <TemporalOroboros@gmail.com>
// SPDX-FileCopyrightText: 2023 TemporalOroboros <temporaloroboros@gmail.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
// SPDX-FileCopyrightText: 2024 SimpleStation14 <130339894+SimpleStation14@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 sleepyyapril <123355664+sleepyyapril@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Content.Server.Administration.Systems;
using Content.Server.Chemistry.Containers.EntitySystems;
using Content.Server.EUI;
using Content.Shared.Administration;
using Content.Shared.Chemistry.Components.SolutionManager;
using Content.Shared.Eui;
using JetBrains.Annotations;
using Robust.Shared.Timing;

namespace Content.Server.Administration.UI
{
    /// <summary>
    ///     Admin Eui for displaying and editing the reagents in a solution.
    /// </summary>
    [UsedImplicitly]
    public sealed class EditSolutionsEui : BaseEui
    {
        [Dependency] private readonly IEntityManager _entityManager = default!;
        [Dependency] private readonly IGameTiming _gameTiming = default!;
        private readonly SolutionContainerSystem _solutionContainerSystem = default!;
        public readonly EntityUid Target;

        public EditSolutionsEui(EntityUid entity)
        {
            IoCManager.InjectDependencies(this);
            _solutionContainerSystem = _entityManager.System<SolutionContainerSystem>();
            Target = entity;
        }

        public override void Opened()
        {
            base.Opened();
            StateDirty();
        }

        public override void Closed()
        {
            base.Closed();
            _entityManager.System<AdminVerbSystem>().OnEditSolutionsEuiClosed(Player, this);
        }

        public override EuiStateBase GetNewState()
        {
            List<(string Name, NetEntity Solution)>? netSolutions;

            if (_entityManager.TryGetComponent(Target, out SolutionContainerManagerComponent? container) && container.Containers.Count > 0)
            {
                netSolutions = new();
                foreach (var (name, solution) in _solutionContainerSystem.EnumerateSolutions((Target, container)))
                {
                    if (name is null || !_entityManager.TryGetNetEntity(solution, out var netSolution))
                        continue;

                    netSolutions.Add((name, netSolution.Value));
                }
            }
            else
                netSolutions = null;

            return new EditSolutionsEuiState(_entityManager.GetNetEntity(Target), netSolutions, _gameTiming.CurTick);
        }
    }
}
