using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using static Dustbuster.App;

namespace Dustbuster
{
    public partial class AccordionPage : ContentPage
    {
        public AccordionPage()
        {
            InitializeComponent();
            MainOne.DataSource = GetSampleData();
            MainOne.DataBind();
        }
        void OnListItemClicked(object o, ItemTappedEventArgs e)
        {

            var vListItem = e.Item as SimpleObject;
            var vMessage = "You Clicked on " + vListItem.TextValue + " With Value " + vListItem.DataValue;
            DisplayAlert("Message", vMessage, "Ok");

        }

        public List<AccordionSource> GetSampleData()
        {
            var vResult = new List<AccordionSource>();

            #region First List View
            var vListOne = new List<SimpleObject>();
            for (int i = 0; i < 6; i++)
            {
                var vObject = new SimpleObject()
                {
                    TextValue = "ObjectNo-" + i.ToString(),
                    DataValue = i.ToString()
                };
                vListOne.Add(vObject);
            }
            var vListViewOne = new ListView()
            {
                ItemsSource = vListOne,
                ItemTemplate = new DataTemplate(typeof(ListDataViewCell))
            };
            vListViewOne.ItemTapped += OnListItemClicked;
            #endregion

            #region Second List
            var vListTwo = new List<SimpleObject>();
            var vObjectRavi = new SimpleObject()
            {
                TextValue = "S Ravi Kumar",
                DataValue = "1"
            };
            vListTwo.Add(vObjectRavi);
            var vObjectFather = new SimpleObject()
            {
                TextValue = "Father",
                DataValue = "2"
            };
            vListTwo.Add(vObjectFather);
            var vObjectTrainer = new SimpleObject()
            {
                TextValue = "Trainer",
                DataValue = "2"
            };
            vListTwo.Add(vObjectTrainer);
            var vObjectConsultant = new SimpleObject()
            {
                TextValue = "Consultant",
                DataValue = "2"
            };
            vListTwo.Add(vObjectConsultant);
            var vObjectArchitect = new SimpleObject()
            {
                TextValue = "Architect",
                DataValue = "2"
            };
            vListTwo.Add(vObjectArchitect);

            var vListViewTwo = new ListView()
            {
                ItemsSource = vListTwo,
                ItemTemplate = new DataTemplate(typeof(ListDataViewCell))
            };
            vListViewTwo.ItemTapped += OnListItemClicked;
            #endregion

            #region StackLayout
            var vViewLayout = new StackLayout()
            {
                Children = {
                    new Label { Text = "Static Content:" },
                    new Label { Text = "Name : S Ravi Kumar" },
                    new Label { Text = "Roles : Father,Trainer,Consultant,Architect" }
                }
            };
            #endregion

            var vFirstAccord = new AccordionSource()
            {
                HeaderText = "First",
                HeaderTextColor = Color.Black,
                HeaderBackGroundColor = Color.Yellow,
                ContentItems = vListViewTwo
            };
            vResult.Add(vFirstAccord);
            var vSecond = new AccordionSource()
            {
                HeaderText = "Second ",
                HeaderTextColor = Color.White,
                HeaderBackGroundColor = Color.FromHex("#77d065"),
                ContentItems = vViewLayout
            };
            vResult.Add(vSecond);
            var vThird = new AccordionSource()
            {
                HeaderText = "Third",
                HeaderTextColor = Color.White,
                HeaderBackGroundColor = Color.Purple,
                ContentItems = vListViewOne
            };
            vResult.Add(vThird);
            return vResult;
        }
    }
}
