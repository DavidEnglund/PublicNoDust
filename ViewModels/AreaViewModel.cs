using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dustbuster
{
    class AreaViewModel : INotifyPropertyChanged
    {
        private double _length = 0, _width = 0;
        private string _length_metrix = METRE_NOTATION, _width_metrix = METRE_NOTATION, _area_metrix = METRE_SQUARE_NOTATION;
        private double _area = 0;

        private readonly static string METRE_NOTATION = "m", KILOMETRE_NOTATION = "km";
        private readonly static string METRE_SQUARE_NOTATION = "m2", KILOMETRE_SQUARE_NOTATION = "km2";

        public AreaViewModel()
        {
            this.CalculateArea = new Command((nothing) =>
            {
                // Add the key to the input string.

                double temp_length = Length;
                double temp_width = Width;

                if (LengthMetrix == KILOMETRE_NOTATION)
                    temp_length = temp_length * 1000;

                if (WidthMetrix == KILOMETRE_NOTATION)
                    temp_width = temp_width * 1000;

                double temp_area = temp_length * temp_width;

                if (AreaMetrix == KILOMETRE_SQUARE_NOTATION)
                    temp_area = temp_area / 1000000;

                Area = temp_area;

            });

            this.ChangeLengthMetrix = new Command((nothing) =>
            {
                if (LengthMetrix == METRE_NOTATION)
                {
                    LengthMetrix = KILOMETRE_NOTATION;
                    Length = Length / 1000;
                }
                else
                {
                    LengthMetrix = METRE_NOTATION;
                    Length = Length * 1000;
                }
            });

            this.ChangeWidthMetrix = new Command((nothing) =>
            {
                if (WidthMetrix == METRE_NOTATION)
                {
                    WidthMetrix = KILOMETRE_NOTATION;
                    Width = Width / 1000;
                }
                else
                {
                    WidthMetrix = METRE_NOTATION;
                    Width = Width * 1000;
                }
            });

            this.ChangeAreaMetrix = new Command((nothing) =>
            {
                if (AreaMetrix == METRE_SQUARE_NOTATION)
                {
                    AreaMetrix = KILOMETRE_SQUARE_NOTATION;
                    Area = Area / 1000000;
                }
                else
                {
                    AreaMetrix = METRE_SQUARE_NOTATION;
                    Area = Area * 1000000;
                }
            });
        }

        public double Length
        {
            get
            {
                return _length;
            }
            set
            {
                if (_length != value)
                {
                    _length = value;
                    OnPropertyChanged("Length");
                }
            }
        }

        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged("Width");
                }
            }
        }

        public double Area
        {
            get
            {
                return _area;
            }
            set
            {
                if (_area != value)
                {
                    _area = value;
                    OnPropertyChanged("Area");
                }
            }
        }

        public string LengthMetrix
        {
            get
            {
                return _length_metrix;
            }
            set
            {
                if (_length_metrix != value)
                {
                    _length_metrix = value;
                    OnPropertyChanged("LengthMetrix");
                }
            }
        }

        public string WidthMetrix
        {
            get
            {
                return _width_metrix;
            }
            set
            {
                if (_width_metrix != value)
                {
                    _width_metrix = value;
                    OnPropertyChanged("WidthMetrix");
                }
            }
        }

        public string AreaMetrix
        {
            get
            {
                return _area_metrix;
            }
            set
            {
                if (_area_metrix != value)
                {
                    _area_metrix = value;
                    OnPropertyChanged("AreaMetrix");
                }
            }
        }

        public ICommand CalculateArea { set; get; }

        public ICommand ChangeLengthMetrix { set; get; }

        public ICommand ChangeWidthMetrix { set; get; }

        public ICommand ChangeAreaMetrix { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
