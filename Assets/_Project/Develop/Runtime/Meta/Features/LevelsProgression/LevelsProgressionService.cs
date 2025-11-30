using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Meta.Features.LevelsProgression
{
    public class LevelsProgressionService
    {
        private const int FirstLevel = 1;

        private readonly List<int> _completedLevels = new();

        public bool IsLeveCompeted(int levelNumber) => _completedLevels.Contains(levelNumber);

        public void AddLeveToCompeted(int levelNumber)
        {
            if(IsLeveCompeted(levelNumber))
                return; 
            
            _completedLevels.Add(levelNumber);
        }

        public bool CanPay(int leveNumber)
        {
            return leveNumber == FirstLevel || PreviousLevelCompleted(leveNumber); 
        }

        private bool PreviousLevelCompleted(int levelNumber) => IsLeveCompeted(levelNumber - 1);
    }
}
