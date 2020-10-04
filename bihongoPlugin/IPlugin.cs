using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bihongoPlugin
{
    public enum MenuPosition {

        // Summary:
        //     The control appears on File menu.
        File,
        // Summary:
        //     The control appears on Edit menu.
        Edit,
        // Summary:
        //     The control appears on Format menu.
        Format,
        // Summary:
        //     The control appears on Developer menu.
        Developer,
        // Summary:
        //     The control appears on Tools menu.
        Tools,
        // Summary:
        //     The control appears on Plugins menu.
        Plugins,
        // Summary:
        //     The control appears on Settings menu.
        Settings,
        // Summary:
        //     The control appears on Help menu.
        Help,
        // Summary:
        //     The control appears on Toolbar.
        Toolbar 
    }
    public enum ContextMenuPosition {
        // Summary:
        //     The control appears on context menu.
        Context_Menu 
    }
    public enum PanelPosition {
        // Summary:
        //     The control appears on panel.
        Bottom_Panel 
    }

    public interface StandardIO
    {
        // Summary:
        //     This define name of plugins.
        string Name { get; }
        // Summary:
        //     This define position of plugins.
        string[] position { get; }
        // Summary:
        //     This is entry point.
        void Start();
    }

}
