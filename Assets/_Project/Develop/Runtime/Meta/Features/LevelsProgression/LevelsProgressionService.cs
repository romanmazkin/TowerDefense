using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Meta.Features.LevelsProgression
{
    public class LevelsProgressionService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private const int FirstLevel = 1;

        private readonly List<int> _completedLevels = new();

        public LevelsProgressionService(PlayerDataProvider playerDataProvider) 
        {
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public bool IsLeveCompeted(int levelNumber) => _completedLevels.Contains(levelNumber);

        public void AddLevelToCompeted(int levelNumber)
        {
            if(IsLeveCompeted(levelNumber))
                return; 
            
            _completedLevels.Add(levelNumber);
        }

        public bool CanPlay(int leveNumber)
        {
            return leveNumber == FirstLevel || PreviousLevelCompleted(leveNumber); 
        }

        private bool PreviousLevelCompleted(int levelNumber) => IsLeveCompeted(levelNumber - 1);

        public void ReadFrom(PlayerData data)
        {
            _completedLevels.Clear();
            _completedLevels.AddRange(data.CompletedLevels);
        }

        public void WriteTo(PlayerData data)
        {
            data.CompletedLevels.Clear();
            data.CompletedLevels.AddRange(_completedLevels);
        }
    }
}
