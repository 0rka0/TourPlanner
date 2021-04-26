using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using TourPlannerModels;
using TourPlannerBL;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using System.IO;

//To be implemented: search function, reading from db

namespace TourPlanner
{
    class ViewModel : INotifyPropertyChanged
    {
        public ITourFactory tourFactory;
        private string _output = "";
        private string _filter;
        private string _start;
        private string _end;
        private string _description;
        private string _information;
        private Tour _curTour;

        private ObservableCollection<Tour> _tourList = new ObservableCollection<Tour>();

        public ObservableCollection<Tour> TourList
        {
            get 
            {
                return _tourList;
            }
            set
            {
                if (_tourList != value)
                {
                    _tourList = value;
                    OnPropertyChanged();
                }
            }
        }

        public Tour CurTour
        {
            get
            {
                return _curTour;
            }
            set
            {
                if (CurTour != value && value != null)
                {
                    _curTour = value;
                    OnPropertyChanged(nameof(CurTour));
                }
            }
        }

        // set itemsource of image

        public string CurTourImage
        {
            get
            {
                if (CurTour?.Id != null)
                {
                    try
                    {
                        string location = $@"{Configuration.ImagePath}{CurTour.Image}";
                        if (File.Exists(location))
                        {
                            var bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                            bitmap.UriSource = new Uri(location);
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.EndInit();

                            //this.logger.LogDebug($"Image source has changed!");

                            return location;
                        }
                    }
                    catch (Exception e)
                    {
                        //this.logger.LogError(e, $"Exception was thrown when setting Tour Map Image: {e}");
                    }
                }

                //this.logger.LogDebug($"Image source has not been found!");
                return null;
            }
        }

        public string FilterInput
        {
            get
            {
                return _filter;
            }
            set
            {
                if (FilterInput != value)
                {
                    _filter = value;
                    OnPropertyChanged(nameof(FilterInput));
                }
            }
        }

        public string StartInput
        {
            get
            {
                return _start;
            }
            set
            {
                if (StartInput != value)
                {
                    _start = value;
                    OnPropertyChanged(nameof(StartInput));
                }
            }
        }

        public string EndInput
        {
            get
            {
                return _end;
            }
            set
            {
                if (EndInput != value)
                {
                    _end = value;
                    OnPropertyChanged(nameof(EndInput));
                }
            }
        }

        public string DescriptionInput
        {
            get
            {
                return _description;
            }
            set
            {
                if (DescriptionInput != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(DescriptionInput));
                }
            }
        }

        public string InformationInput
        {
            get
            {
                return _information;
            }
            set
            {
                if (InformationInput != value)
                {
                    _information = value;
                    OnPropertyChanged(nameof(InformationInput));
                }
            }
        }

        public string FilterOutput
        {
            get
            {
                return _output;
            }
            set
            {
                if(_output != value)
                {
                    _output = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ExecuteSearch { get; }

        public ICommand ExecuteClear { get; }

        public ICommand ExecuteAdd { get; }

        public ICommand ExecuteDel { get; }

        public ICommand ExecuteEdit { get; }

        public ICommand ExecuteImport { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            this.ExecuteSearch = new ExecuteSearch(this);
            this.ExecuteClear = new ExecuteClear(this);
            this.ExecuteAdd = new ExecuteAdd(this);
            this.ExecuteDel = new ExecuteDel(this);
            this.ExecuteEdit = new ExecuteEdit(this);
            this.ExecuteImport = new ExecuteImport(this);

            InitTourList();
        }

        private void InitTourList()
        {
            tourFactory = TourFactory.GetInstance();

            TourList = new ObservableCollection<Tour>();
            FillTourList();
        }

        public void FillTourList()
        {
            foreach (Tour tour in tourFactory.GetTours())
            {
                TourList.Add(tour);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
