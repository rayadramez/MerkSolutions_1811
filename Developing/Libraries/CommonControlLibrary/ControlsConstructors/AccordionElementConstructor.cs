using System.Collections.Generic;

namespace CommonControlLibrary.ControlsConstructors
{
	public class AccordionElementConstructor
	{
		public bool IsExpanded { get; set; }
		public string Text { get; set; }
		public List<AccordionElementConstructor> AccordionElementConstructorsList { get; set; }

		public AccordionElementConstructor(bool isExpanded, string text,
			List<AccordionElementConstructor> accordionElementConstructorsList)
		{
			IsExpanded = isExpanded;
			Text = text;
			AccordionElementConstructorsList = accordionElementConstructorsList;
		}
	}
}
