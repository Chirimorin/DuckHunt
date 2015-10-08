using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DuckHunt.Containers
{
    public class GraphicsContainer
    {
        private object graphicsListLock = new object();

        private List<UIElement> _addedGraphics;
        private List<UIElement> _removedGraphics;

        public GraphicsContainer()
        {
            _addedGraphics = new List<UIElement>();
            _removedGraphics = new List<UIElement>();
        }

        public void AddGraphic(UIElement element)
        {
            lock(graphicsListLock)
            {
                _addedGraphics.Add(element);
            }
        }

        public void RemoveGraphic(UIElement element)
        {
            lock (graphicsListLock)
            {
                _removedGraphics.Add(element);
            }
        }

        public void UpdateGraphics(Canvas canvas)
        {
            lock(graphicsListLock)
            {
                foreach(UIElement element in _addedGraphics)
                {
                    canvas.Children.Add(element);
                }
                _addedGraphics.Clear();

                foreach(UIElement element in _removedGraphics)
                {
                    canvas.Children.Remove(element);
                }
                _removedGraphics.Clear();
            }
        }
    }
}
