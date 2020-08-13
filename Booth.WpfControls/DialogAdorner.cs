using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Booth.WpfControls
{
    class DialogAdorner : Adorner
    {
        private UIElement _AdornedElement;
        private VisualCollection _Visuals;
        private ContentPresenter _ContentPresenter;

        public DialogAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            _AdornedElement = adornedElement;
            _Visuals = new VisualCollection(this);
            _ContentPresenter = new ContentPresenter();
            _Visuals.Add(_ContentPresenter);
        }

        public DialogAdorner(UIElement adornedElement, Visual content)
            : this(adornedElement)
        {
            Content = content;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return constraint;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _ContentPresenter.Arrange(WindowRect()); 
            return _ContentPresenter.RenderSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _Visuals[index];
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return _Visuals.Count;
            }
        }

        public object Content
        {
            get
            {
                return _ContentPresenter.Content;
            }
            set
            {
                _ContentPresenter.Content = value;
            }
        }


        private Rect WindowRect()
        {
            if (_AdornedElement == null)
            {
                throw new ArgumentException("Can't adorn a null control");
            }
            else
            {
                // Get a point of the offset of the window
                Window window = Application.Current.MainWindow;
                Point windowOffset;

                if (window == null)
                {
                    throw new ArgumentException("Can't get main window");
                }
                else
                {
                    GeneralTransform transformToAncestor = _AdornedElement.TransformToAncestor(window);
                    if (transformToAncestor == null || transformToAncestor.Inverse == null)
                    {
                        throw new ArgumentException("No transform to window");
                    }
                    else
                    {
                        windowOffset = transformToAncestor.Inverse.Transform(new Point(0, 0));
                    }
                }

                // Get a point of the lower-right corner of the window
                Point windowLowerRight = windowOffset;
                windowLowerRight.Offset(window.ActualWidth, window.ActualHeight);
                return new Rect(windowOffset, windowLowerRight);
            }
        }
    }
}
