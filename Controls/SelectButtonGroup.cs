using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dustbuster
{    
    public class SelectButtonGroup
    {
        // the list of buttons in the group
        private List<SelectImageButton> group;
        // the selected index's private variable
        private int selectedIndex = 0;
       
        // the public interface for setting the selected control
        public SelectImageButton Selected
        {
            get { return group[selectedIndex]; }
            set
            {
                // set the requested buttton to be selected then deselect the rest
                selectedIndex = group.IndexOf(value);
                value.Selected = true;
                foreach (SelectImageButton checkForSelected in group)
                {
                    // the button wont let itself be set to false if the group has it as selected
                    checkForSelected.Selected = false;
                }               
            }
        }
        // another interface that is a function
        public void SetSelected(SelectImageButton selectMe)
        {
            Selected = selectMe;
        }
        // and a function version for the get as well
        public SelectImageButton GetSelected()
        {
            return Selected;
        }
        // interfaces for the selected index
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if(value > 0 && value < group.Count)
                {
                    Selected = group[value];
                    selectedIndex = value;
                }
            }
        }

		public void UnselectAll()
		{
			SelectImageButton currentSelected = this.Selected;
			currentSelected.Selected = false;
			selectedIndex = 0;
			currentSelected = null;
		}

        public SelectButtonGroup()
        {
            group = new List<SelectImageButton>();
        }
        // adding a button to the group
        public void AddButton(SelectImageButton addedButton)
        {
            if (!group.Contains(addedButton))
            {
                group.Add(addedButton);
                addedButton.ButtonGroup = this;
            }
        }
        // removing a button from the group
        public void RemoveButton(SelectImageButton addedButton)
        {
            if (group.Contains(addedButton))
            {
                group.Remove(addedButton);
                addedButton.ButtonGroup = null;
            }
        }
    }
}
