// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Game.Graphics;
using osu.Game.Rulesets.Scoring;
using osu.Game.Scoring;

namespace osu.Game.Rulesets.Mods
{
    public abstract class ModSafetyNet : Mod, IApplicableToHealthProcessor, IApplicableFailOverride, IApplicableToScoreProcessor
    {
        public override string Name => "Safety Net";
        public override string Acronym => "SN";
        public override IconUsage? Icon => OsuIcon.ModNoFail;
        public override ModType Type => ModType.DifficultyReduction;
        public override LocalisableString Description => "Like NF, but better.";
        public override double ScoreMultiplier => 1;
        public override Type[] IncompatibleMods => new[] { typeof(ModNoFail), typeof(ModRelax), typeof(ModFailCondition) };

        private bool hasFailed { get; set; }

        public void ApplyToHealthProcessor(HealthProcessor healthProcessor)
        {
            healthProcessor.Health.BindValueChanged(value =>
            {
                if (value.NewValue == 0)
                {
                    hasFailed = true;
                }
            });
        }

        public bool PerformFail() => false;

        public bool RestartOnFail => false;

        public void ApplyToScoreProcessor(ScoreProcessor scoreProcessor)
        {
        }

        public ScoreRank AdjustRank(ScoreRank rank, double accuracy)
        {
            return hasFailed ? ScoreRank.F : rank;
        }
    }
}
