using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using SkiInfoSystem.Utils;

namespace SkiInfoSystem.Core
{
    public class Controller
    {
        #region Fields
        public IEnumerable<Slope> Slopes { get;private set; }

        public event EventHandler<string> ActualSlopesConditions;

        private CsvFileDataProvider _slopes;

        #endregion
        public Controller()
        {
            _slopes = new CsvFileDataProvider();
            Slopes = _slopes.GetSlops();
            RegistrationForConditionsUpdated();
        }

        public void RegistrationForConditionsUpdated()
        {
            foreach (Slope slopes in Slopes)
            {
                slopes.ConditionsUpdated += OnConditionsUpdated;
            }
        }

        private void OnConditionsUpdated(object sender, string massege)
        {
            
        }

        #region Methods

        #endregion

    }
}
