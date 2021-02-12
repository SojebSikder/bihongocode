using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bihongoPlugin
{
    public enum MenuPosition {

        /// <summary>
        /// The control appears on File menu.
        /// </summary>
        File,
        /// <summary>
        /// The control appears on Edit menu.
        /// </summary>
        Edit,  
        /// <summary>
        /// The control appears on Format menu.
        /// </summary>
        Format,
        /// <summary>
        /// The control appears on Developer menu.
        /// </summary>
        Developer,  
        /// <summary>
        /// The control appears on Tools menu.
        /// </summary>
        Tools,
        /// <summary>
        /// The control appears on Plugins menu.
        /// </summary>
        Plugins,
        /// <summary>
        /// The control appears on Settings menu.
        /// </summary>
        Settings,   
        /// <summary>
        /// The control appears on Help menu.
        /// </summary>
        Help, 
        /// <summary>
        /// The control appears on Toolbar.
        /// </summary>
        Toolbar 
    }
    public enum ContextMenuPosition {
        /// <summary>
        /// The control appears on context menu.
        /// </summary>
        Context_Menu 
    }
    public enum PanelPosition {
        /// <summary>
        /// The control appears on panel.
        /// </summary>
        Bottom_Panel 
    }

    public interface StandardIO
    { 
        /// <summary>
        /// This define name of plugins.
        /// </summary>
        string Name { get; }  
        /// <summary>
        /// This define position of plugins.
        /// </summary>
        string[] position { get; }
        /// <summary>
        /// This is entry point.
        /// </summary>
        void Start();
    }

}
