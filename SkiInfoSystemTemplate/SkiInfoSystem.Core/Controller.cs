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

        private CsvFileDataProvider _slopes;

        #endregion
        public Controller()
        {
            _slopes = new CsvFileDataProvider();
            Slopes = _slopes.GetSlops();
        }

        #region Methods

        #endregion

    }
}
