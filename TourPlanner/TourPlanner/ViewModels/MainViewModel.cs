using System.Windows.Input;
using System.Collections.ObjectModel;
using TourPlannerModels.TourObject;
using TourPlannerBL.TourObjectHandling;
using System.Windows.Media.Imaging;
using System;
using System.IO;
using log4net;
using System.Reflection;
using System.Windows.Media;
using TourPlanner.Commands;
using System.Collections.Generic;
using System.Windows.Data;
using System.Configuration;

//To be implemented: search function, reading from db

namespace TourPlanner.Viewmodels
{
    class MainViewModel : ViewModelBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private string _filterOutput = "";
        private string _filterInput;
        private string _error;
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

        private ObservableCollection<Attraction> _curAttractionList = new ObservableCollection<Attraction>();

        public ObservableCollection<Attraction> CurAttractionList
        {
            get
            {
                return _curAttractionList;
            }
            set
            {
                if (CurAttractionList != value)
                {
                    _curAttractionList = value;
                    OnPropertyChanged();
                }
            }
        }

        private CollectionView _ratingList = new CollectionView(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

        public CollectionView RatingList
        {
            get
            {
                return _ratingList;
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
                    RefreshAttractionList();
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
                    try
                    {
                        string location = Path.GetFullPath(TourPlannerModels.Configuration.ImagePath + CurTour.Image);
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
                return _filterInput;
            }
            set
            {
                if (FilterInput != value)
                {
                    _filterInput = value;
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
                return _filterOutput;
            }
            set
            {
                if(_filterOutput != value)
                {
                    _filterOutput = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ErrorOutput
        {
            get
            {
                return _error;
            }
            set
            {
                if(_error != value)
                {
                    _error = value;
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

        public ICommand ExecuteExport { get; }

        public ICommand ExecuteCreateReport { get; }

        public ICommand ExecuteCreateSummary { get; }

        public ICommand ExecuteTourLogAdd { get; }
        
        public ICommand ExecuteTourLogEdit { get; }

        public ICommand ExecuteTourLogDel { get; }

        public ICommand ExecuteExit { get; }

        public MainViewModel()
        {
            this.ExecuteSearch = new ExecuteSearch(this);
            this.ExecuteClear = new ExecuteClear(this);
            this.ExecuteAdd = new ExecuteTourAdd(this);
            this.ExecuteDel = new ExecuteTourDel(this);
            this.ExecuteEdit = new ExecuteTourEdit(this);
            this.EnableExecuteEdit = new EnableExecuteTourEdit(this);
            this.ExecuteCopy = new ExecuteTourCopy(this);
            this.ExecuteImport = new ExecuteImport(this);
            this.ExecuteExport = new ExecuteExport(this);
            this.ExecuteCreateReport = new ExecuteCreateReport(this);
            this.ExecuteCreateSummary = new ExecuteCreateSummary(this);
            this.ExecuteTourLogAdd = new ExecuteTourLogAdd(this);
            this.ExecuteTourLogEdit = new ExecuteTourLogEdit(this);
            this.ExecuteTourLogDel = new ExecuteTourLogDel(this);
            this.ExecuteExit = new ExecuteExit(this);

            _logger.Info("Application initialized");

            Configure();
            DbInitiator.Init();
            InitTourList();
        }

        private void Configure()
        {
            TourPlannerModels.Configuration.Configure(ConfigurationManager.AppSettings);
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
            if (_curTour != null)
            {
                foreach (TourLog log in _curTour.LogList)
                {
                    CurLogList.Add(log);
                }
            }
        }

        public void FillAttractionList()
        {
            if (_curTour != null)
            {
                foreach (Attraction attraction in _curTour.AttList)
                {
                    CurAttractionList.Add(attraction);
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

        public void RefreshAttractionList()
        {
            CurAttractionList.Clear();
            FillAttractionList();
        }

        public void RefreshCurTourLoglist()
        {
            if (_curTour != null)
            {
                _curTour.LogList.Clear();
                _curTour.LogList.AddRange((List<TourLog>)TourLogSelector.SelectTourLogsById(_curTour.Id));
            }
        }
    }
}
