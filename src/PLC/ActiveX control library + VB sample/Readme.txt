========================================================================
Automated Solutions' Automation Direct Ethernet ActiveX Control
========================================================================
v2.1.2 5/25/2011

1. Fixed problem with Result property on AsyncRefresh with no device 
   connected. If AsyncRefresh was called prior to calling SyncRefresh 
   and a communications error occurred, the Result property returned 0.

========================================================================
v2.1.1 12/13/2007

1. Fixed problem with memory address computation on GX, SP, and CT types.

========================================================================
v2.1.0 7/16/2007

1. Added BCD Methods Get/SetDataWordBCDM and Get/SetDataLongBCDM. 

2. Added BCD Property Arrays: DataWordBCD and DataLongBCD. 

3. Added 16 and 32-bit BCD data formats to MiniHMI example application.

4. Bumped version resource to indicate interface has been changed.

5. Converted help system to HTMLHelp for Vista compatibility.

6. Modified control to use HTMLHelp for contact sensitive help.

7. Modernized MiniHMI example application.

========================================================================
v2.0a 2/24/2006

1. Fixed problem with SetDataFloatM, SetDataLongM, and SetDataPtrM methods. 
Residual buffer value was written instead of intended data

========================================================================

v2.0 9/15/2003

1. This is the first shipping version of this control.

========================================================================