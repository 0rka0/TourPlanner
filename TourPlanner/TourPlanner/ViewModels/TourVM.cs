using System.Windows.Input;
using System.Collections.ObjectModel;
using TourPlannerModels.TourObject;
using TourPlannerBL.TourObjectHandling;
using TourPlannerModels;
using System.Windows.Media.Imaging;
using System;
using System.IO;
using log4net;
using System.Reflection;
using System.Windows.Media;
using TourPlanner.Commands;
using System.Collections.Generic;

//To be implemented: search function, reading from db

namespace TourPlanner.Viewmodels
{
    class TourVM : ViewModelBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private string _output = "";
        private string _filter;
        private string _start;
        private string _end;
        private string _description;
        private string _information;
        private Tour _curTour;
        private TourLog _curTourLog;

        private ObservableCollection<Tour> _tourList = new ObservableCollection<Tour>();

        public ObservableCollection<Tour> TourList
        {
            get 
            {
                return _tourList;
            }
            set
            {
                if (TourList != value)
                {
                    _tourList = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<TourLog> _curLogList = new ObservableCollection<TourLog>();

        public ObservableCollection<TourLog> CurLogList
        {
            get
            {
                return _curLogList;
            }
            set
            {
                if (CurLogList != value)
                {
                    _curLogList = value;
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
                if (CurTour != value)
                {
                    _curTour = value;
                    RefreshLogList();
                    OnPropertyChanged(nameof(CurTour));
                    OnPropertyChanged(nameof(CurTourImage));
                }
            }
        }

        public TourLog CurTourLog
        {
            get
            {
                return _curTourLog;
            }
            set
            {
                if (CurTourLog != value)
                {
                    _curTourLog = value;
                    OnPropertyChanged(nameof(CurTourLog));
                }
            }
        }

        // set itemsource of image

        public ImageSource CurTourImage
        {
            get
            {
                if (CurTour?.Id != null)
                {
                    _logger.Info("Attempting to load image");

                    try
                    {
                        string location = Path.GetFullPath(Configuration.ImagePath + CurTour.Image);
                        if (File.Exists(location))
                        {
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                            bitmap.UriSource = new Uri(location);
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.EndInit();

                            return bitmap;
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error("Accessing file led to following error: " + e.Message);
                    }
                }

                _logger.Warn("Image source has not been found!");
                OnPropertyChanged(nameof(CurTour));
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

        public ICommand EnableExecuteEdit { get; }

        public ICommand ExecuteEdit { get; }

        public ICommand ExecuteCopy { get; }

        public ICommand ExecuteImport { get; }

        public ICommand ExecuteCreateReport { get; }

        public ICommand ExecuteTourLogAdd { get; }
        
        public ICommand ExecuteTourLogEdit { get; }

        public ICommand ExecuteTourLogDel { get; }

        public TourVM()
        {
            this.ExecuteSearch = new ExecuteSearch(this);
            this.ExecuteClear = new ExecuteClear(this);
            this.ExecuteAdd = new ExecuteTourAdd(this);
            this.ExecuteDel = new ExecuteTourDel(this);
            this.ExecuteEdit = new ExecuteTourEdit(this);
            this.EnableExecuteEdit = new EnableExecuteTourEdit(this);
            this.ExecuteCopy = new ExecuteTourCopy(this);
            this.ExecuteImport = new ExecuteImport(this);
            this.ExecuteCreateReport = new ExecuteCreateReport(this);
            this.ExecuteTourLogAdd = new ExecuteTourLogAdd(this);
            this.ExecuteTourLogEdit = new ExecuteTourLogEdit(this);
            this.ExecuteTourLogDel = new ExecuteTourLogDel(this);

            _logger.Info("Application initialized");

            InitTourList();
        }

        private void InitTourList()
        {
            _logger.Info("TourList initialized");

            TourList = new ObservableCollection<Tour>();
            FillTourList();
        }

        public void FillTourList()
        {
            _logger.Info("TourList updated");

            foreach (Tour tour in TourSelector.GetTours())
            {
                TourList.Add(tour);
            }
        }

        public void FillLogList()
        {
            _logger.Info("LogList updated");

            if (_curTour != null)
            {
                foreach (TourLog log in _curTour.LogList)
                {
                    CurLogList.Add(log);
                }
            }
        }

        public void RefreshTourList()
        {
            TourList.Clear();
            FillTourList();
        }

        public void RefreshLogList()
        {
            RefreshCurTourLoglist();
            CurLogList.Clear();
            FillLogList();
        }

        public void RefreshCurTourLoglist()
        {
            if (_curTour != null)
            {
                _curTour.LogList.Clear();
                _curTour.LogList.AddRange((List<TourLog>)TourSelector.SelectTourLogsById(_curTour.Id));
            }
        }
    }
}
