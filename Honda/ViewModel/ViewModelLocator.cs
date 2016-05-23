/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Honda.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Honda.Model;

namespace Honda.ViewModel
{
    /// <summary>
    /// ViewModel 统一的管理器 
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ExampleVM>();
            SimpleIoc.Default.Register<LoginVM>();
            SimpleIoc.Default.Register<StoreTourVM>();
            SimpleIoc.Default.Register<EvaluationOfTourVM>();
            SimpleIoc.Default.Register<PreviewVM>();
            //SimpleIoc.Default.Register<ServiceBasedEvaluationVM>();
            //SimpleIoc.Default.Register<ServiceEvaluationManageVM>();
            //SimpleIoc.Default.Register<ComponentEvaluateVM>();
            //SimpleIoc.Default.Register<SuggestPlusesVM>();
            SimpleIoc.Default.Register<MainVM>();
            SimpleIoc.Default.Register<ScheduleManageVM>();
            SimpleIoc.Default.Register<WorkLightspotAndIdeaVM>();
            SimpleIoc.Default.Register<BusinessPolicyVM>();
            SimpleIoc.Default.Register<ImproveVM>();
            SimpleIoc.Default.Register<ImproveCheckVM>();
            SimpleIoc.Default.Register<SummaryVM>();
            SimpleIoc.Default.Register<UnivesalEvaluateVM>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }


        public ExampleVM Example
        {
            get { return ServiceLocator.Current.GetInstance<ExampleVM>(); }
        }


        public LoginVM Login
        {
            get { return ServiceLocator.Current.GetInstance<LoginVM>(); }
        }


        public StoreTourVM StoreTour
        {
            get { return ServiceLocator.Current.GetInstance<StoreTourVM>(); }
        }

        public EvaluationOfTourVM EvaluationOfTour
        {
            get { return ServiceLocator.Current.GetInstance<EvaluationOfTourVM>(); }
        }

        public PreviewVM Preview
        {
            get { return ServiceLocator.Current.GetInstance<PreviewVM>(); }
        }

        //public ServiceBasedEvaluationVM ServiceBasedEvaluation
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<ServiceBasedEvaluationVM>();
        //    }
        //}


        //public ServiceEvaluationManageVM ServiceEvaluationManage
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<ServiceEvaluationManageVM>();
        //    }
        //}

        //public ComponentEvaluateVM ComponentEvaluate
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<ComponentEvaluateVM>();
        //    }
        //}

        //public SuggestPlusesVM SuggestPluses
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<SuggestPlusesVM>();
        //    }
        //}


        public UnivesalEvaluateVM UniversalEvaluate
        {
            get { return ServiceLocator.Current.GetInstance<UnivesalEvaluateVM>(); }
        }

        public MainVM MainV
        {
            get { return ServiceLocator.Current.GetInstance<MainVM>(); }
        }

        public ScheduleManageVM ScheduleManage
        {
            get { return ServiceLocator.Current.GetInstance<ScheduleManageVM>(); }
        }

        public WorkLightspotAndIdeaVM WorkLightspotAndIdea
        {
            get { return ServiceLocator.Current.GetInstance<WorkLightspotAndIdeaVM>(); }
        }

        public BusinessPolicyVM BusinessPolicy
        {
            get { return ServiceLocator.Current.GetInstance<BusinessPolicyVM>(); }
        }

        public ImproveVM Improve
        {
            get { return ServiceLocator.Current.GetInstance<ImproveVM>(); }
        }

        public ImproveCheckVM ImproveCheck
        {
            get { return ServiceLocator.Current.GetInstance<ImproveCheckVM>(); }
        }


        public SummaryVM Summary
        {
            get { return ServiceLocator.Current.GetInstance<SummaryVM>(); }
        }

        /// <summary>
        /// Cleans up all the resources.  
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}