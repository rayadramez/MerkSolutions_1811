using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;

namespace CommonControlLibrary.ControlsConstructors
{
	public class AccordionControlConstructor
	{
		public Control ParentControlToAttach { get; set; }
		public DockStyle DockStyle { get; set; }
		public ScrollBarMode ScrollBarMode { get; set; } 
		public List<AccordionElementConstructor> AccordionElementConstructorsList { get; set; }

		public AccordionControlConstructor(Control parentControlToAttach, DockStyle dockStyle, ScrollBarMode scrollBarMode,
			List<AccordionElementConstructor> accordionElementConstructorsList)
		{
			ParentControlToAttach = parentControlToAttach;
			DockStyle = dockStyle;
			ScrollBarMode = scrollBarMode;
			AccordionElementConstructorsList = accordionElementConstructorsList;
		}
	}
}
