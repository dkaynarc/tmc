VERSION 5.00
Object = "{BDC217C8-ED16-11CD-956C-0000C04E4C0A}#1.1#0"; "TABCTL32.OCX"
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "MSFLXGRD.OCX"
Object = "{2EF652C8-51F9-4346-9F83-E3394A20AA66}#2.1#0"; "Asadtcp.OCX"
Begin VB.Form frmMainASADTCP 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "MiniHMI Example Application for Automated Solutions A-D Ethernet ActiveX Control"
   ClientHeight    =   7155
   ClientLeft      =   1005
   ClientTop       =   3165
   ClientWidth     =   11955
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   700
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "MiniHMI.frx":0000
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   ScaleHeight     =   477
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   797
   Begin TabDlg.SSTab SSTab1 
      Height          =   7155
      Left            =   0
      TabIndex        =   17
      Top             =   0
      Width           =   11955
      _ExtentX        =   21087
      _ExtentY        =   12621
      _Version        =   393216
      Tabs            =   5
      Tab             =   4
      TabsPerRow      =   5
      TabHeight       =   635
      BackColor       =   12632256
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      TabCaption(0)   =   "Welcome"
      TabPicture(0)   =   "MiniHMI.frx":0442
      Tab(0).ControlEnabled=   0   'False
      Tab(0).Control(0)=   "Frame14"
      Tab(0).Control(1)=   "Picture1"
      Tab(0).Control(2)=   "Text4"
      Tab(0).ControlCount=   3
      TabCaption(1)   =   "Read V Memory"
      TabPicture(1)   =   "MiniHMI.frx":045E
      Tab(1).ControlEnabled=   0   'False
      Tab(1).Control(0)=   "Frame1"
      Tab(1).Control(1)=   "GridRead"
      Tab(1).Control(2)=   "Frame2"
      Tab(1).Control(3)=   "txtMessageRead"
      Tab(1).Control(4)=   "Frame12"
      Tab(1).ControlCount=   5
      TabCaption(2)   =   "Write V Memory"
      TabPicture(2)   =   "MiniHMI.frx":047A
      Tab(2).ControlEnabled=   0   'False
      Tab(2).Control(0)=   "Frame4"
      Tab(2).Control(1)=   "Frame3"
      Tab(2).Control(2)=   "GridWrite"
      Tab(2).Control(3)=   "txtMessageWrite"
      Tab(2).Control(4)=   "Frame13"
      Tab(2).ControlCount=   5
      TabCaption(3)   =   "Read Discretes"
      TabPicture(3)   =   "MiniHMI.frx":0496
      Tab(3).ControlEnabled=   0   'False
      Tab(3).Control(0)=   "Frame5"
      Tab(3).Control(0).Enabled=   0   'False
      Tab(3).Control(1)=   "Frame6"
      Tab(3).Control(1).Enabled=   0   'False
      Tab(3).Control(2)=   "Frame7"
      Tab(3).Control(2).Enabled=   0   'False
      Tab(3).Control(3)=   "txtMessageReadD"
      Tab(3).Control(3).Enabled=   0   'False
      Tab(3).Control(4)=   "Frame15"
      Tab(3).Control(4).Enabled=   0   'False
      Tab(3).ControlCount=   5
      TabCaption(4)   =   "Write Discretes"
      TabPicture(4)   =   "MiniHMI.frx":04B2
      Tab(4).ControlEnabled=   -1  'True
      Tab(4).Control(0)=   "Frame8"
      Tab(4).Control(0).Enabled=   0   'False
      Tab(4).Control(1)=   "Frame9"
      Tab(4).Control(1).Enabled=   0   'False
      Tab(4).Control(2)=   "Frame10"
      Tab(4).Control(2).Enabled=   0   'False
      Tab(4).Control(3)=   "txtMessageWriteD"
      Tab(4).Control(3).Enabled=   0   'False
      Tab(4).Control(4)=   "Frame16"
      Tab(4).Control(4).Enabled=   0   'False
      Tab(4).ControlCount=   5
      Begin VB.Frame Frame16 
         Height          =   1095
         Left            =   180
         TabIndex        =   404
         Top             =   480
         Width           =   2835
         Begin VB.CommandButton cmdSyncWriteD 
            Caption         =   "Sync Write"
            Height          =   375
            Left            =   1440
            TabIndex        =   406
            Top             =   180
            Width           =   1275
         End
         Begin VB.CommandButton cmdAsyncWriteD 
            Caption         =   "Async Write"
            Height          =   375
            Left            =   120
            TabIndex        =   405
            Top             =   180
            Width           =   1275
         End
      End
      Begin VB.Frame Frame15 
         Height          =   1095
         Left            =   -74820
         TabIndex        =   398
         Top             =   480
         Width           =   2835
         Begin VB.CheckBox chkAutoPollReadD 
            Caption         =   "Auto Poll"
            ForeColor       =   &H00C00000&
            Height          =   375
            Left            =   120
            TabIndex        =   403
            ToolTipText     =   "Check this box to enable auto-polling"
            Top             =   660
            Width           =   1335
         End
         Begin VB.TextBox txtAutoPollIntervalReadD 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   1440
            TabIndex        =   401
            Top             =   660
            Width           =   735
         End
         Begin VB.CommandButton cmdAsyncReadD 
            Caption         =   "Async Read"
            Height          =   375
            Left            =   120
            TabIndex        =   400
            Top             =   180
            Width           =   1275
         End
         Begin VB.CommandButton cmdSyncReadD 
            Caption         =   "Sync Read"
            Height          =   375
            Left            =   1440
            TabIndex        =   399
            Top             =   180
            Width           =   1275
         End
         Begin VB.Label Label2 
            AutoSize        =   -1  'True
            Caption         =   "mSec"
            ForeColor       =   &H00C00000&
            Height          =   195
            Left            =   2220
            TabIndex        =   402
            Top             =   720
            Width           =   495
         End
      End
      Begin VB.Frame Frame13 
         Height          =   1095
         Left            =   -74820
         TabIndex        =   395
         Top             =   480
         Width           =   2835
         Begin VB.CommandButton cmdSyncWrite 
            Caption         =   "Sync Write"
            Height          =   375
            Left            =   1440
            TabIndex        =   397
            Top             =   180
            Width           =   1275
         End
         Begin VB.CommandButton cmdAsyncWrite 
            Caption         =   "Async Write"
            Height          =   375
            Left            =   120
            TabIndex        =   396
            Top             =   180
            Width           =   1275
         End
      End
      Begin VB.Frame Frame12 
         Height          =   1095
         Left            =   -74820
         TabIndex        =   389
         Top             =   480
         Width           =   2835
         Begin VB.TextBox txtAutoPollIntervalRead 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   285
            Left            =   1440
            TabIndex        =   407
            Top             =   660
            Width           =   735
         End
         Begin VB.CheckBox chkAutoPollRead 
            Caption         =   "Auto Poll"
            ForeColor       =   &H00C00000&
            Height          =   375
            Left            =   120
            TabIndex        =   392
            ToolTipText     =   "Check this box to enable auto-polling"
            Top             =   660
            Width           =   1335
         End
         Begin VB.CommandButton cmdSyncRead 
            Caption         =   "Sync Read"
            Height          =   375
            Left            =   1440
            TabIndex        =   391
            Top             =   180
            Width           =   1275
         End
         Begin VB.CommandButton cmdAsyncRead 
            Caption         =   "Async Read"
            Height          =   375
            Left            =   120
            TabIndex        =   390
            Top             =   180
            Width           =   1275
         End
         Begin VB.Label lblPollRateRead 
            AutoSize        =   -1  'True
            Caption         =   "Rate"
            ForeColor       =   &H00C00000&
            Height          =   255
            Left            =   540
            TabIndex        =   394
            ToolTipText     =   "Enter poll rate for auto-polling in mSec"
            Top             =   720
            Width           =   495
         End
         Begin VB.Label Label8 
            AutoSize        =   -1  'True
            Caption         =   "mSec"
            ForeColor       =   &H00C00000&
            Height          =   195
            Left            =   2220
            TabIndex        =   393
            ToolTipText     =   "Enter poll rate for auto-polling in mSec"
            Top             =   720
            Width           =   495
         End
      End
      Begin VB.Frame Frame14 
         Height          =   2835
         Left            =   -65880
         TabIndex        =   381
         Top             =   3780
         Width           =   2532
         Begin VB.Label lblHelp 
            AutoSize        =   -1  'True
            Caption         =   "View the Help File"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   -1  'True
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000002&
            Height          =   192
            Left            =   180
            TabIndex        =   386
            ToolTipText     =   "Click to open Help system."
            Top             =   1320
            Width           =   1284
         End
         Begin VB.Label lblReadme 
            AutoSize        =   -1  'True
            Caption         =   "View the Readme File"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   -1  'True
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000002&
            Height          =   192
            Left            =   180
            TabIndex        =   385
            ToolTipText     =   "Click to view Readme file"
            Top             =   840
            Width           =   1572
         End
         Begin VB.Label lblOrdering 
            AutoSize        =   -1  'True
            Caption         =   "Online Product Information"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   -1  'True
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000002&
            Height          =   192
            Left            =   180
            TabIndex        =   384
            ToolTipText     =   "Click to go to the A-B Ethernet ActiveX Control product page"
            Top             =   1800
            Width           =   1860
         End
         Begin VB.Label lblWebsite 
            Alignment       =   2  'Center
            AutoSize        =   -1  'True
            Caption         =   "Automated Solutions Website"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   -1  'True
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000002&
            Height          =   192
            Left            =   180
            TabIndex        =   383
            ToolTipText     =   "Click here to go to Automated Solutions' website."
            Top             =   2280
            Width           =   2112
         End
         Begin VB.Label lblVersion 
            AutoSize        =   -1  'True
            Caption         =   "ActiveX Control Version"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   -1  'True
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H80000002&
            Height          =   192
            Left            =   180
            TabIndex        =   382
            ToolTipText     =   "Click for ActiveX Control version"
            Top             =   360
            Width           =   1692
         End
      End
      Begin VB.TextBox txtMessageReadD 
         Height          =   2055
         Left            =   -71820
         Locked          =   -1  'True
         MultiLine       =   -1  'True
         TabIndex        =   347
         Text            =   "MiniHMI.frx":04CE
         Top             =   4980
         Width           =   8595
      End
      Begin VB.TextBox txtMessageWriteD 
         Height          =   2055
         Left            =   3180
         Locked          =   -1  'True
         MultiLine       =   -1  'True
         TabIndex        =   346
         Text            =   "MiniHMI.frx":064F
         Top             =   4980
         Width           =   8535
      End
      Begin VB.Frame Frame10 
         Caption         =   "State"
         Height          =   4335
         Left            =   3180
         TabIndex        =   15
         ToolTipText     =   "Check boxes to set bits, clear boxes to reset bits"
         Top             =   480
         Width           =   8535
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   0
            Left            =   5610
            TabIndex        =   313
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   1
            Left            =   5340
            TabIndex        =   312
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   2
            Left            =   5070
            TabIndex        =   311
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   3
            Left            =   4800
            TabIndex        =   310
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   4
            Left            =   4530
            TabIndex        =   309
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   5
            Left            =   4260
            TabIndex        =   308
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   6
            Left            =   3990
            TabIndex        =   307
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   7
            Left            =   3720
            TabIndex        =   306
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   8
            Left            =   2370
            TabIndex        =   305
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   9
            Left            =   2100
            TabIndex        =   304
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   10
            Left            =   1830
            TabIndex        =   303
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   11
            Left            =   1560
            TabIndex        =   302
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   12
            Left            =   1290
            TabIndex        =   301
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   13
            Left            =   1020
            TabIndex        =   300
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   14
            Left            =   750
            TabIndex        =   299
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   15
            Left            =   480
            TabIndex        =   298
            Top             =   720
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   16
            Left            =   5610
            TabIndex        =   297
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   17
            Left            =   5340
            TabIndex        =   296
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   18
            Left            =   5070
            TabIndex        =   295
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   19
            Left            =   4800
            TabIndex        =   294
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   20
            Left            =   4530
            TabIndex        =   293
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   21
            Left            =   4260
            TabIndex        =   292
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   22
            Left            =   3990
            TabIndex        =   291
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   23
            Left            =   3720
            TabIndex        =   290
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   24
            Left            =   2370
            TabIndex        =   289
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   25
            Left            =   2100
            TabIndex        =   288
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   26
            Left            =   1830
            TabIndex        =   287
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   27
            Left            =   1560
            TabIndex        =   286
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   28
            Left            =   1290
            TabIndex        =   285
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   29
            Left            =   1020
            TabIndex        =   284
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   30
            Left            =   750
            TabIndex        =   283
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   31
            Left            =   480
            TabIndex        =   282
            Top             =   900
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   32
            Left            =   5610
            TabIndex        =   281
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   33
            Left            =   5340
            TabIndex        =   280
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   34
            Left            =   5070
            TabIndex        =   279
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   35
            Left            =   4800
            TabIndex        =   278
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   36
            Left            =   4530
            TabIndex        =   277
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   37
            Left            =   4260
            TabIndex        =   276
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   38
            Left            =   3990
            TabIndex        =   275
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   39
            Left            =   3720
            TabIndex        =   274
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   40
            Left            =   2370
            TabIndex        =   273
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   41
            Left            =   2100
            TabIndex        =   272
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   42
            Left            =   1830
            TabIndex        =   271
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   43
            Left            =   1560
            TabIndex        =   270
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   44
            Left            =   1290
            TabIndex        =   269
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   45
            Left            =   1020
            TabIndex        =   268
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   46
            Left            =   750
            TabIndex        =   267
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   47
            Left            =   480
            TabIndex        =   266
            Top             =   1080
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   48
            Left            =   5610
            TabIndex        =   265
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   49
            Left            =   5340
            TabIndex        =   264
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   50
            Left            =   5070
            TabIndex        =   263
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   51
            Left            =   4800
            TabIndex        =   262
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   52
            Left            =   4530
            TabIndex        =   261
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   53
            Left            =   4260
            TabIndex        =   260
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   54
            Left            =   3990
            TabIndex        =   259
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   55
            Left            =   3720
            TabIndex        =   258
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   56
            Left            =   2370
            TabIndex        =   257
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   57
            Left            =   2100
            TabIndex        =   256
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   58
            Left            =   1830
            TabIndex        =   255
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   59
            Left            =   1560
            TabIndex        =   254
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   60
            Left            =   1290
            TabIndex        =   253
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   61
            Left            =   1020
            TabIndex        =   252
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   62
            Left            =   750
            TabIndex        =   251
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   63
            Left            =   480
            TabIndex        =   250
            Top             =   1260
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   64
            Left            =   5610
            TabIndex        =   249
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   65
            Left            =   5340
            TabIndex        =   248
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   66
            Left            =   5070
            TabIndex        =   247
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   67
            Left            =   4800
            TabIndex        =   246
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   68
            Left            =   4530
            TabIndex        =   245
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   69
            Left            =   4260
            TabIndex        =   244
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   70
            Left            =   3990
            TabIndex        =   243
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   71
            Left            =   3720
            TabIndex        =   242
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   72
            Left            =   2370
            TabIndex        =   241
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   73
            Left            =   2100
            TabIndex        =   240
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   74
            Left            =   1830
            TabIndex        =   239
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   75
            Left            =   1560
            TabIndex        =   238
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   76
            Left            =   1290
            TabIndex        =   237
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   77
            Left            =   1020
            TabIndex        =   236
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   78
            Left            =   750
            TabIndex        =   235
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   79
            Left            =   480
            TabIndex        =   234
            Top             =   1440
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   80
            Left            =   5610
            TabIndex        =   233
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   81
            Left            =   5340
            TabIndex        =   232
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   82
            Left            =   5070
            TabIndex        =   231
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   83
            Left            =   4800
            TabIndex        =   230
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   84
            Left            =   4530
            TabIndex        =   229
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   85
            Left            =   4260
            TabIndex        =   228
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   86
            Left            =   3990
            TabIndex        =   227
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   87
            Left            =   3720
            TabIndex        =   226
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   88
            Left            =   2370
            TabIndex        =   225
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   89
            Left            =   2100
            TabIndex        =   224
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   90
            Left            =   1830
            TabIndex        =   223
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   91
            Left            =   1560
            TabIndex        =   222
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   92
            Left            =   1290
            TabIndex        =   221
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   93
            Left            =   1020
            TabIndex        =   220
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   94
            Left            =   750
            TabIndex        =   219
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   95
            Left            =   480
            TabIndex        =   218
            Top             =   1620
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   96
            Left            =   5610
            TabIndex        =   217
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   97
            Left            =   5340
            TabIndex        =   216
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   98
            Left            =   5070
            TabIndex        =   215
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   99
            Left            =   4800
            TabIndex        =   214
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   100
            Left            =   4530
            TabIndex        =   213
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   101
            Left            =   4260
            TabIndex        =   212
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   102
            Left            =   3990
            TabIndex        =   211
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   103
            Left            =   3720
            TabIndex        =   210
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   104
            Left            =   2370
            TabIndex        =   209
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   105
            Left            =   2100
            TabIndex        =   208
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   106
            Left            =   1830
            TabIndex        =   207
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   107
            Left            =   1560
            TabIndex        =   206
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   108
            Left            =   1290
            TabIndex        =   205
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   109
            Left            =   1020
            TabIndex        =   204
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   110
            Left            =   750
            TabIndex        =   203
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   111
            Left            =   480
            TabIndex        =   202
            Top             =   1800
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   112
            Left            =   5610
            TabIndex        =   201
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   113
            Left            =   5340
            TabIndex        =   200
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   114
            Left            =   5070
            TabIndex        =   199
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   115
            Left            =   4800
            TabIndex        =   198
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   116
            Left            =   4530
            TabIndex        =   197
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   117
            Left            =   4260
            TabIndex        =   196
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   118
            Left            =   3990
            TabIndex        =   195
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   119
            Left            =   3720
            TabIndex        =   194
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   120
            Left            =   2370
            TabIndex        =   193
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   121
            Left            =   2100
            TabIndex        =   192
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   122
            Left            =   1830
            TabIndex        =   191
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   123
            Left            =   1560
            TabIndex        =   190
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   124
            Left            =   1290
            TabIndex        =   189
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   125
            Left            =   1020
            TabIndex        =   188
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   126
            Left            =   750
            TabIndex        =   187
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   127
            Left            =   480
            TabIndex        =   186
            Top             =   1980
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   128
            Left            =   5610
            TabIndex        =   185
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   129
            Left            =   5340
            TabIndex        =   184
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   130
            Left            =   5070
            TabIndex        =   183
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   131
            Left            =   4800
            TabIndex        =   182
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   132
            Left            =   4530
            TabIndex        =   181
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   133
            Left            =   4260
            TabIndex        =   180
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   134
            Left            =   3990
            TabIndex        =   179
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   135
            Left            =   3720
            TabIndex        =   178
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   136
            Left            =   2370
            TabIndex        =   177
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   137
            Left            =   2100
            TabIndex        =   176
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   138
            Left            =   1830
            TabIndex        =   175
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   139
            Left            =   1560
            TabIndex        =   174
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   140
            Left            =   1290
            TabIndex        =   173
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   141
            Left            =   1020
            TabIndex        =   172
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   142
            Left            =   750
            TabIndex        =   171
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   143
            Left            =   480
            TabIndex        =   170
            Top             =   2160
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   144
            Left            =   5610
            TabIndex        =   169
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   145
            Left            =   5340
            TabIndex        =   168
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   146
            Left            =   5070
            TabIndex        =   167
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   147
            Left            =   4800
            TabIndex        =   166
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   148
            Left            =   4530
            TabIndex        =   165
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   149
            Left            =   4260
            TabIndex        =   164
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   150
            Left            =   3990
            TabIndex        =   163
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   151
            Left            =   3720
            TabIndex        =   162
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   152
            Left            =   2370
            TabIndex        =   161
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   153
            Left            =   2100
            TabIndex        =   160
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   154
            Left            =   1830
            TabIndex        =   159
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   155
            Left            =   1560
            TabIndex        =   158
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   156
            Left            =   1290
            TabIndex        =   157
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   157
            Left            =   1020
            TabIndex        =   156
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   158
            Left            =   750
            TabIndex        =   155
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   159
            Left            =   480
            TabIndex        =   154
            Top             =   2340
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   160
            Left            =   5610
            TabIndex        =   153
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   161
            Left            =   5340
            TabIndex        =   152
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   162
            Left            =   5070
            TabIndex        =   151
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   163
            Left            =   4800
            TabIndex        =   150
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   164
            Left            =   4530
            TabIndex        =   149
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   165
            Left            =   4260
            TabIndex        =   148
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   166
            Left            =   3990
            TabIndex        =   147
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   167
            Left            =   3720
            TabIndex        =   146
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   168
            Left            =   2370
            TabIndex        =   145
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   169
            Left            =   2100
            TabIndex        =   144
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   170
            Left            =   1830
            TabIndex        =   143
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   171
            Left            =   1560
            TabIndex        =   142
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   172
            Left            =   1290
            TabIndex        =   141
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   173
            Left            =   1020
            TabIndex        =   140
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   174
            Left            =   750
            TabIndex        =   139
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   175
            Left            =   480
            TabIndex        =   138
            Top             =   2520
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   176
            Left            =   5610
            TabIndex        =   137
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   177
            Left            =   5340
            TabIndex        =   136
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   178
            Left            =   5070
            TabIndex        =   135
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   179
            Left            =   4800
            TabIndex        =   134
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   180
            Left            =   4530
            TabIndex        =   133
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   181
            Left            =   4260
            TabIndex        =   132
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   182
            Left            =   3990
            TabIndex        =   131
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   183
            Left            =   3720
            TabIndex        =   130
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   184
            Left            =   2370
            TabIndex        =   129
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   185
            Left            =   2100
            TabIndex        =   128
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   186
            Left            =   1830
            TabIndex        =   127
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   187
            Left            =   1560
            TabIndex        =   126
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   188
            Left            =   1290
            TabIndex        =   125
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   189
            Left            =   1020
            TabIndex        =   124
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   190
            Left            =   750
            TabIndex        =   123
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   191
            Left            =   480
            TabIndex        =   122
            Top             =   2700
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   192
            Left            =   5610
            TabIndex        =   121
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   193
            Left            =   5340
            TabIndex        =   120
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   194
            Left            =   5070
            TabIndex        =   119
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   195
            Left            =   4800
            TabIndex        =   118
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   196
            Left            =   4530
            TabIndex        =   117
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   197
            Left            =   4260
            TabIndex        =   116
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   198
            Left            =   3990
            TabIndex        =   115
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   199
            Left            =   3720
            TabIndex        =   114
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   200
            Left            =   2370
            TabIndex        =   113
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   201
            Left            =   2100
            TabIndex        =   112
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   202
            Left            =   1830
            TabIndex        =   111
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   203
            Left            =   1560
            TabIndex        =   110
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   204
            Left            =   1290
            TabIndex        =   109
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   205
            Left            =   1020
            TabIndex        =   108
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   206
            Left            =   750
            TabIndex        =   107
            Top             =   2880
            Width           =   195
         End
         Begin VB.CheckBox Check1 
            Caption         =   "Check1"
            Height          =   195
            Index           =   207
            Left            =   480
            TabIndex        =   106
            Top             =   2880
            Width           =   195
         End
         Begin VB.Label Label48 
            AutoSize        =   -1  'True
            Caption         =   "Bit"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   9
            Left            =   1440
            TabIndex        =   388
            Top             =   240
            Width           =   255
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "01"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   2790
            TabIndex        =   363
            Top             =   720
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "03"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   3
            Left            =   2790
            TabIndex        =   362
            Top             =   900
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "05"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   5
            Left            =   2790
            TabIndex        =   361
            Top             =   1080
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "07"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   7
            Left            =   2790
            TabIndex        =   360
            Top             =   1260
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "11"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   9
            Left            =   2790
            TabIndex        =   359
            Top             =   1440
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "13"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   11
            Left            =   2790
            TabIndex        =   358
            Top             =   1620
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "15"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   13
            Left            =   2790
            TabIndex        =   357
            Top             =   1800
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "17"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   15
            Left            =   2790
            TabIndex        =   356
            Top             =   1980
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "21"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   17
            Left            =   2790
            TabIndex        =   355
            Top             =   2160
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "23"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   19
            Left            =   2790
            TabIndex        =   354
            Top             =   2340
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "25"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   21
            Left            =   2790
            TabIndex        =   353
            Top             =   2520
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "27"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   23
            Left            =   2790
            TabIndex        =   352
            Top             =   2700
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "29"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   25
            Left            =   2790
            TabIndex        =   351
            Top             =   2880
            Width           =   225
         End
         Begin VB.Label Label48 
            AutoSize        =   -1  'True
            Caption         =   "Byte"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   8
            Left            =   2700
            TabIndex        =   350
            Top             =   480
            Width           =   390
         End
         Begin VB.Label Label48 
            AutoSize        =   -1  'True
            Caption         =   "Byte"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   5
            Left            =   5970
            TabIndex        =   349
            Top             =   480
            Width           =   390
         End
         Begin VB.Label Label48 
            AutoSize        =   -1  'True
            Caption         =   "Bit"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   4
            Left            =   4620
            TabIndex        =   348
            Top             =   240
            Width           =   255
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "30"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   24
            Left            =   6060
            TabIndex        =   342
            Top             =   2880
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "26"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   22
            Left            =   6060
            TabIndex        =   341
            Top             =   2700
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "24"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   20
            Left            =   6060
            TabIndex        =   340
            Top             =   2520
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "22"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   18
            Left            =   6060
            TabIndex        =   339
            Top             =   2340
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "20"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   16
            Left            =   6060
            TabIndex        =   338
            Top             =   2160
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "16"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   14
            Left            =   6060
            TabIndex        =   337
            Top             =   1980
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "14"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   12
            Left            =   6060
            TabIndex        =   336
            Top             =   1800
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "12"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   10
            Left            =   6060
            TabIndex        =   335
            Top             =   1620
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "10"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   8
            Left            =   6060
            TabIndex        =   334
            Top             =   1440
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "06"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   6
            Left            =   6060
            TabIndex        =   333
            Top             =   1260
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "04"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   4
            Left            =   6060
            TabIndex        =   332
            Top             =   1080
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "02"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   2
            Left            =   6060
            TabIndex        =   331
            Top             =   900
            Width           =   225
         End
         Begin VB.Label lblWriteDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "00"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   6060
            TabIndex        =   330
            Top             =   720
            Width           =   225
         End
         Begin VB.Label Label48 
            AutoSize        =   -1  'True
            Caption         =   "7"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   510
            TabIndex        =   329
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label47 
            AutoSize        =   -1  'True
            Caption         =   "6"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   780
            TabIndex        =   328
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label46 
            AutoSize        =   -1  'True
            Caption         =   "5"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   1050
            TabIndex        =   327
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label45 
            AutoSize        =   -1  'True
            Caption         =   "4"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   1320
            TabIndex        =   326
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label44 
            AutoSize        =   -1  'True
            Caption         =   "3"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   1590
            TabIndex        =   325
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label43 
            AutoSize        =   -1  'True
            Caption         =   "2"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   1860
            TabIndex        =   324
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label42 
            AutoSize        =   -1  'True
            Caption         =   "1"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   2130
            TabIndex        =   323
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label41 
            AutoSize        =   -1  'True
            Caption         =   "0"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   2400
            TabIndex        =   322
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label40 
            AutoSize        =   -1  'True
            Caption         =   "7"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   3750
            TabIndex        =   321
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label39 
            AutoSize        =   -1  'True
            Caption         =   "6"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   4020
            TabIndex        =   320
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label38 
            AutoSize        =   -1  'True
            Caption         =   "5"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   4290
            TabIndex        =   319
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label37 
            AutoSize        =   -1  'True
            Caption         =   "4"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   4560
            TabIndex        =   318
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label36 
            AutoSize        =   -1  'True
            Caption         =   "3"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   4830
            TabIndex        =   317
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label35 
            AutoSize        =   -1  'True
            Caption         =   "2"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   5100
            TabIndex        =   316
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label34 
            AutoSize        =   -1  'True
            Caption         =   "1"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   5370
            TabIndex        =   315
            Top             =   480
            Width           =   120
         End
         Begin VB.Label Label33 
            AutoSize        =   -1  'True
            Caption         =   "0"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   5640
            TabIndex        =   314
            Top             =   480
            Width           =   120
         End
      End
      Begin VB.Frame Frame7 
         Caption         =   "State"
         Height          =   4335
         Left            =   -71820
         TabIndex        =   16
         ToolTipText     =   "Displays status of selected discrete inputs/outputs"
         Top             =   480
         Width           =   8595
         Begin VB.Label Label48 
            AutoSize        =   -1  'True
            Caption         =   "Bit"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   7
            Left            =   1440
            TabIndex        =   387
            Top             =   240
            Width           =   255
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "31"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   25
            Left            =   2790
            TabIndex        =   378
            Top             =   2880
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "27"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   23
            Left            =   2790
            TabIndex        =   377
            Top             =   2700
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "25"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   21
            Left            =   2790
            TabIndex        =   376
            Top             =   2520
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "23"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   19
            Left            =   2790
            TabIndex        =   375
            Top             =   2340
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "21"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   17
            Left            =   2790
            TabIndex        =   374
            Top             =   2160
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "17"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   15
            Left            =   2790
            TabIndex        =   373
            Top             =   1980
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "15"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   13
            Left            =   2790
            TabIndex        =   372
            Top             =   1800
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "13"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   11
            Left            =   2790
            TabIndex        =   371
            Top             =   1620
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "11"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   9
            Left            =   2790
            TabIndex        =   370
            Top             =   1440
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "07"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   7
            Left            =   2790
            TabIndex        =   369
            Top             =   1260
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "05"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   5
            Left            =   2790
            TabIndex        =   368
            Top             =   1080
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "03"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   3
            Left            =   2790
            TabIndex        =   367
            Top             =   900
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "01"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   2790
            TabIndex        =   366
            Top             =   720
            Width           =   225
         End
         Begin VB.Label Label48 
            AutoSize        =   -1  'True
            Caption         =   "Byte"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   6
            Left            =   2700
            TabIndex        =   365
            Top             =   480
            Width           =   390
         End
         Begin VB.Label Label48 
            AutoSize        =   -1  'True
            Caption         =   "Bit"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   2
            Left            =   4620
            TabIndex        =   364
            Top             =   240
            Width           =   255
         End
         Begin VB.Label Label48 
            AutoSize        =   -1  'True
            Caption         =   "Byte"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   3
            Left            =   5970
            TabIndex        =   343
            Top             =   480
            Width           =   390
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   16
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label33 
            AutoSize        =   -1  'True
            Caption         =   "0"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   5640
            TabIndex        =   105
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   17
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label34 
            AutoSize        =   -1  'True
            Caption         =   "1"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   5370
            TabIndex        =   104
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   18
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label35 
            AutoSize        =   -1  'True
            Caption         =   "2"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   5100
            TabIndex        =   103
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   19
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label36 
            AutoSize        =   -1  'True
            Caption         =   "3"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   4830
            TabIndex        =   102
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   20
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label37 
            AutoSize        =   -1  'True
            Caption         =   "4"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   4560
            TabIndex        =   101
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   21
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label38 
            AutoSize        =   -1  'True
            Caption         =   "5"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   4290
            TabIndex        =   100
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   22
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label39 
            AutoSize        =   -1  'True
            Caption         =   "6"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   4020
            TabIndex        =   99
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   23
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label40 
            AutoSize        =   -1  'True
            Caption         =   "+7"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   3660
            TabIndex        =   98
            Top             =   480
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   24
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label41 
            AutoSize        =   -1  'True
            Caption         =   "0"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   2400
            TabIndex        =   97
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   25
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label42 
            AutoSize        =   -1  'True
            Caption         =   "1"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   2130
            TabIndex        =   96
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   26
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label43 
            AutoSize        =   -1  'True
            Caption         =   "2"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   1860
            TabIndex        =   95
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   27
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label44 
            AutoSize        =   -1  'True
            Caption         =   "3"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   1590
            TabIndex        =   94
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   28
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label45 
            AutoSize        =   -1  'True
            Caption         =   "4"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   1320
            TabIndex        =   93
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   29
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label46 
            AutoSize        =   -1  'True
            Caption         =   "5"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   1050
            TabIndex        =   92
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   30
            Left            =   750
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label47 
            AutoSize        =   -1  'True
            Caption         =   "6"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   780
            TabIndex        =   91
            Top             =   480
            Width           =   120
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   31
            Left            =   480
            Shape           =   3  'Circle
            Top             =   900
            Width           =   165
         End
         Begin VB.Label Label48 
            AutoSize        =   -1  'True
            Caption         =   "+7"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   420
            TabIndex        =   90
            Top             =   480
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   0
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   1
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   2
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   3
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   4
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   5
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   6
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   7
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   8
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   9
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   10
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   11
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   12
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   13
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   14
            Left            =   750
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   15
            Left            =   480
            Shape           =   3  'Circle
            Top             =   720
            Width           =   165
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "00"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   6060
            TabIndex        =   89
            Top             =   720
            Width           =   225
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "02"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   2
            Left            =   6060
            TabIndex        =   88
            Top             =   900
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   32
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   33
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   34
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   35
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   36
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   37
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   38
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   39
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   40
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   41
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   42
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   43
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   44
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   45
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   46
            Left            =   750
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   47
            Left            =   480
            Shape           =   3  'Circle
            Top             =   1080
            Width           =   165
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "04"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   4
            Left            =   6060
            TabIndex        =   87
            Top             =   1080
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   48
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   49
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   50
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   51
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   52
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   53
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   54
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   55
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   56
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   57
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   58
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   59
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   60
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   61
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   62
            Left            =   750
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   63
            Left            =   480
            Shape           =   3  'Circle
            Top             =   1260
            Width           =   165
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "06"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   6
            Left            =   6060
            TabIndex        =   86
            Top             =   1260
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   64
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   65
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   66
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   67
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   68
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   69
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   70
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   71
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   72
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   73
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   74
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   75
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   76
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   77
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   78
            Left            =   750
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   79
            Left            =   480
            Shape           =   3  'Circle
            Top             =   1440
            Width           =   165
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "10"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   8
            Left            =   6060
            TabIndex        =   85
            Top             =   1440
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   80
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   81
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   82
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   83
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   84
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   85
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   86
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   87
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   88
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   89
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   90
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   91
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   92
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   93
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   94
            Left            =   750
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   95
            Left            =   480
            Shape           =   3  'Circle
            Top             =   1620
            Width           =   165
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "12"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   10
            Left            =   6060
            TabIndex        =   84
            Top             =   1620
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   96
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   97
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   98
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   99
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   100
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   101
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   102
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   103
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   104
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   105
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   106
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   107
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   108
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   109
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   110
            Left            =   750
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   111
            Left            =   480
            Shape           =   3  'Circle
            Top             =   1800
            Width           =   165
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "14"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   12
            Left            =   6060
            TabIndex        =   83
            Top             =   1800
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   112
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   113
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   114
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   115
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   116
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   117
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   118
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   119
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   120
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   121
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   122
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   123
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   124
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   125
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   126
            Left            =   750
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   127
            Left            =   480
            Shape           =   3  'Circle
            Top             =   1980
            Width           =   165
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "16"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   14
            Left            =   6060
            TabIndex        =   82
            Top             =   1980
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   128
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   129
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   130
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   131
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   132
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   133
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   134
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   135
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   136
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   137
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   138
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   139
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   140
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   141
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   142
            Left            =   750
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   143
            Left            =   480
            Shape           =   3  'Circle
            Top             =   2160
            Width           =   165
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "20"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   16
            Left            =   6060
            TabIndex        =   81
            Top             =   2160
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   144
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   145
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   146
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   147
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   148
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   149
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   150
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   151
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   152
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   153
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   154
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   155
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   156
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   157
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   158
            Left            =   750
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   159
            Left            =   480
            Shape           =   3  'Circle
            Top             =   2340
            Width           =   165
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "22"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   18
            Left            =   6060
            TabIndex        =   80
            Top             =   2340
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   160
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   161
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   162
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   163
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   164
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   165
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   166
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   167
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   168
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   169
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   170
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   171
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   172
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   173
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   174
            Left            =   750
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   175
            Left            =   480
            Shape           =   3  'Circle
            Top             =   2520
            Width           =   165
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "24"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   20
            Left            =   6060
            TabIndex        =   79
            Top             =   2520
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   176
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   177
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   178
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   179
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   180
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   181
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   182
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   183
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   184
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   185
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   186
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   187
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   188
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   189
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   190
            Left            =   750
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   191
            Left            =   480
            Shape           =   3  'Circle
            Top             =   2700
            Width           =   165
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "26"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   22
            Left            =   6060
            TabIndex        =   78
            Top             =   2700
            Width           =   225
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   192
            Left            =   5610
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   193
            Left            =   5340
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   194
            Left            =   5070
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   195
            Left            =   4800
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   196
            Left            =   4530
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   197
            Left            =   4260
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   198
            Left            =   3990
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   199
            Left            =   3720
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   200
            Left            =   2370
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   201
            Left            =   2100
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   202
            Left            =   1830
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   203
            Left            =   1560
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   204
            Left            =   1290
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   205
            Left            =   1020
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   206
            Left            =   750
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Shape LED1 
            FillColor       =   &H000000FF&
            Height          =   165
            Index           =   207
            Left            =   480
            Shape           =   3  'Circle
            Top             =   2880
            Width           =   165
         End
         Begin VB.Label lblReadDiscretes 
            AutoSize        =   -1  'True
            Caption         =   "30"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   24
            Left            =   6060
            TabIndex        =   77
            Top             =   2880
            Width           =   225
         End
      End
      Begin VB.Frame Frame9 
         Caption         =   "Communications"
         Height          =   2115
         Left            =   180
         TabIndex        =   71
         Top             =   4920
         Width           =   2835
         Begin VB.CommandButton cmdDisconnectWriteD 
            Caption         =   "Disconnect"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   1620
            TabIndex        =   420
            Top             =   1080
            Width           =   1095
         End
         Begin VB.TextBox txtTimeoutTransWriteD 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   414
            ToolTipText     =   "Enter the IP Address or network node name of the target host."
            Top             =   1080
            Width           =   735
         End
         Begin VB.TextBox txtNodeAddressWriteD 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   14
            ToolTipText     =   "Enter the IP Address or network node name of the target host."
            Top             =   480
            Width           =   2595
         End
         Begin VB.TextBox txtPollsWriteD 
            BackColor       =   &H8000000F&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   1620
            Locked          =   -1  'True
            TabIndex        =   74
            Text            =   "0"
            ToolTipText     =   "Transaction poll counter"
            Top             =   1680
            Width           =   1095
         End
         Begin VB.TextBox txtResultWriteD 
            BackColor       =   &H8000000F&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            Locked          =   -1  'True
            TabIndex        =   73
            Text            =   "0"
            ToolTipText     =   "Result of last transaction"
            Top             =   1680
            Width           =   735
         End
         Begin VB.TextBox txtErrorsWriteD 
            BackColor       =   &H8000000F&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   72
            Text            =   "0"
            ToolTipText     =   "Transaction error counter"
            Top             =   1680
            Width           =   795
         End
         Begin ASADTCPLib.Asadtcp AsadtcpWriteD 
            Left            =   2100
            Top             =   180
            _Version        =   131073
            _ExtentX        =   609
            _ExtentY        =   556
            _StockProps     =   0
            DataMode        =   1
            Function        =   1
         End
         Begin VB.Label Label6 
            AutoSize        =   -1  'True
            Caption         =   "mSec"
            ForeColor       =   &H00C00000&
            Height          =   195
            Left            =   960
            TabIndex        =   416
            ToolTipText     =   "Enter poll rate for auto-polling in mSec"
            Top             =   1200
            Width           =   495
         End
         Begin VB.Label Label18 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Trans timeout"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   10
            Left            =   180
            TabIndex        =   415
            Top             =   840
            Width           =   1200
         End
         Begin VB.Label Label18 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Node Address"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   7
            Left            =   180
            TabIndex        =   76
            Top             =   240
            Width           =   1215
         End
         Begin VB.Label Label18 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Result    Errors      Polls"
            ForeColor       =   &H00000000&
            Height          =   195
            Index           =   6
            Left            =   180
            TabIndex        =   75
            Top             =   1440
            Width           =   2055
         End
      End
      Begin VB.Frame Frame8 
         Caption         =   "Data"
         Height          =   3135
         Left            =   180
         TabIndex        =   65
         Top             =   1680
         Width           =   2835
         Begin VB.TextBox txtMemQtyWriteD 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   13
            Text            =   "1"
            ToolTipText     =   "Specify the number of 8-bit bytes to write."
            Top             =   1140
            Width           =   795
         End
         Begin VB.TextBox txtMemStartWriteD 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   12
            Text            =   "0"
            ToolTipText     =   $"MiniHMI.frx":07D1
            Top             =   540
            Width           =   1275
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   372
            Index           =   19
            Left            =   5340
            TabIndex        =   69
            Text            =   "11"
            Top             =   1380
            Width           =   972
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   372
            Index           =   18
            Left            =   5340
            TabIndex        =   68
            Text            =   "11"
            Top             =   1020
            Width           =   972
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   372
            Index           =   17
            Left            =   5340
            TabIndex        =   67
            Text            =   "1"
            Top             =   660
            Width           =   972
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   372
            Index           =   16
            Left            =   5340
            TabIndex        =   66
            Text            =   "1"
            Top             =   300
            Width           =   972
         End
         Begin VB.Label lblMemAddrWriteD 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Memory Address (octal)"
            ForeColor       =   &H00C00000&
            Height          =   195
            Left            =   120
            TabIndex        =   345
            Top             =   300
            Width           =   2535
         End
         Begin VB.Label Label3 
            Appearance      =   0  'Flat
            Caption         =   "Quantity (8-bit Bytes)"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   7
            Left            =   120
            TabIndex        =   344
            Top             =   900
            Width           =   2535
         End
         Begin VB.Label Label1 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            Caption         =   "MemAddr"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000C0&
            Height          =   255
            Index           =   4
            Left            =   5340
            TabIndex        =   70
            Top             =   0
            Width           =   975
         End
      End
      Begin VB.Frame Frame6 
         Caption         =   "Communications"
         Height          =   2115
         Left            =   -74820
         TabIndex        =   59
         Top             =   4920
         Width           =   2835
         Begin VB.CommandButton cmdDisconnectReadD 
            Caption         =   "Disconnect"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   1620
            TabIndex        =   421
            Top             =   1080
            Width           =   1095
         End
         Begin VB.TextBox txtTimeoutTransReadD 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   417
            ToolTipText     =   "Enter the IP Address or network node name of the target host."
            Top             =   1080
            Width           =   735
         End
         Begin VB.TextBox txtErrorsReadD 
            BackColor       =   &H8000000F&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   62
            Text            =   "0"
            ToolTipText     =   "Transaction error counter"
            Top             =   1680
            Width           =   795
         End
         Begin VB.TextBox txtResultReadD 
            BackColor       =   &H8000000F&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            Locked          =   -1  'True
            TabIndex        =   61
            Text            =   "0"
            ToolTipText     =   "Result of last transaction"
            Top             =   1680
            Width           =   735
         End
         Begin VB.TextBox txtPollsReadD 
            BackColor       =   &H8000000F&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   1620
            Locked          =   -1  'True
            TabIndex        =   60
            Text            =   "0"
            ToolTipText     =   "Transaction poll counter"
            Top             =   1680
            Width           =   1095
         End
         Begin VB.TextBox txtNodeAddressReadD 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   11
            ToolTipText     =   "Enter the IP Address or network node name of the target host."
            Top             =   480
            Width           =   2595
         End
         Begin ASADTCPLib.Asadtcp AsadtcpReadD 
            Left            =   2100
            Top             =   180
            _Version        =   131073
            _ExtentX        =   609
            _ExtentY        =   556
            _StockProps     =   0
            DataMode        =   1
            Function        =   1
         End
         Begin VB.Label Label7 
            AutoSize        =   -1  'True
            Caption         =   "mSec"
            ForeColor       =   &H00C00000&
            Height          =   195
            Left            =   900
            TabIndex        =   419
            ToolTipText     =   "Enter poll rate for auto-polling in mSec"
            Top             =   1200
            Width           =   495
         End
         Begin VB.Label Label18 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Transaction timeout"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   11
            Left            =   180
            TabIndex        =   418
            Top             =   840
            Width           =   1725
         End
         Begin VB.Label Label18 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Result    Errors      Polls"
            ForeColor       =   &H00000000&
            Height          =   195
            Index           =   3
            Left            =   180
            TabIndex        =   64
            Top             =   1440
            Width           =   2055
         End
         Begin VB.Label Label18 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Node Address"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   2
            Left            =   180
            TabIndex        =   63
            Top             =   240
            Width           =   1215
         End
      End
      Begin VB.Frame Frame5 
         Caption         =   "Data"
         Height          =   3135
         Left            =   -74820
         TabIndex        =   51
         Top             =   1680
         Width           =   2835
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   372
            Index           =   15
            Left            =   5340
            TabIndex        =   55
            Text            =   "1"
            Top             =   300
            Width           =   972
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   372
            Index           =   14
            Left            =   5340
            TabIndex        =   54
            Text            =   "1"
            Top             =   660
            Width           =   972
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   372
            Index           =   13
            Left            =   5340
            TabIndex        =   53
            Text            =   "11"
            Top             =   1020
            Width           =   972
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   372
            Index           =   12
            Left            =   5340
            TabIndex        =   52
            Text            =   "11"
            Top             =   1380
            Width           =   972
         End
         Begin VB.TextBox txtMemStartReadD 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   9
            Text            =   "0"
            ToolTipText     =   $"MiniHMI.frx":088D
            Top             =   540
            Width           =   1275
         End
         Begin VB.TextBox txtMemQtyReadD 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   10
            Text            =   "1"
            ToolTipText     =   "Specify the number of 8-bit bytes to read."
            Top             =   1140
            Width           =   795
         End
         Begin VB.Label Label1 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            Caption         =   "MemAddr"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000C0&
            Height          =   255
            Index           =   2
            Left            =   5340
            TabIndex        =   58
            Top             =   0
            Width           =   975
         End
         Begin VB.Label Label3 
            Appearance      =   0  'Flat
            Caption         =   "Quantity (8-bit Bytes)"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   5
            Left            =   120
            TabIndex        =   57
            Top             =   900
            Width           =   2535
         End
         Begin VB.Label lblMemAddrReadD 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Memory Address (octal)"
            ForeColor       =   &H00C00000&
            Height          =   195
            Left            =   120
            TabIndex        =   56
            Top             =   300
            Width           =   2535
         End
      End
      Begin VB.TextBox txtMessageWrite 
         Height          =   2055
         Left            =   -71820
         MultiLine       =   -1  'True
         TabIndex        =   48
         Text            =   "MiniHMI.frx":0949
         Top             =   4980
         Width           =   8595
      End
      Begin VB.TextBox txtMessageRead 
         Height          =   2055
         Left            =   -71820
         MultiLine       =   -1  'True
         TabIndex        =   42
         Text            =   "MiniHMI.frx":09FA
         Top             =   4980
         Width           =   8595
      End
      Begin VB.Frame Frame2 
         Caption         =   "Communications"
         Height          =   2115
         Left            =   -74820
         TabIndex        =   34
         Top             =   4920
         Width           =   2835
         Begin VB.CommandButton cmdDisconnectRead 
            Caption         =   "Disconnect"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   1620
            TabIndex        =   423
            Top             =   1080
            Width           =   1095
         End
         Begin VB.TextBox txtTimeoutTransRead 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   408
            ToolTipText     =   "Enter the IP Address or network node name of the target host."
            Top             =   1080
            Width           =   735
         End
         Begin VB.TextBox txtNodeAddressRead 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   3
            ToolTipText     =   "Enter the IP Address or network node name of the target host."
            Top             =   480
            Width           =   2595
         End
         Begin ASADTCPLib.Asadtcp AsadtcpRead 
            Left            =   2100
            Top             =   180
            _Version        =   131073
            _ExtentX        =   609
            _ExtentY        =   556
            _StockProps     =   0
            DataMode        =   1
            Function        =   1
         End
         Begin VB.TextBox txtPollsRead 
            BackColor       =   &H8000000F&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   1620
            Locked          =   -1  'True
            TabIndex        =   37
            Text            =   "0"
            ToolTipText     =   "Transaction poll counter"
            Top             =   1680
            Width           =   1095
         End
         Begin VB.TextBox txtResultRead 
            BackColor       =   &H8000000F&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            Locked          =   -1  'True
            TabIndex        =   36
            Text            =   "0"
            ToolTipText     =   "Result of last transaction"
            Top             =   1680
            Width           =   735
         End
         Begin VB.TextBox txtErrorsRead 
            BackColor       =   &H8000000F&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   35
            Text            =   "0"
            ToolTipText     =   "Transaction error counter"
            Top             =   1680
            Width           =   795
         End
         Begin VB.Label Label4 
            AutoSize        =   -1  'True
            Caption         =   "mSec"
            ForeColor       =   &H00C00000&
            Height          =   195
            Left            =   900
            TabIndex        =   410
            ToolTipText     =   "Enter poll rate for auto-polling in mSec"
            Top             =   1200
            Width           =   495
         End
         Begin VB.Label Label18 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Transaction timeout"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   8
            Left            =   180
            TabIndex        =   409
            Top             =   840
            Width           =   1725
         End
         Begin VB.Label Label18 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Node Address"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   180
            TabIndex        =   39
            Top             =   240
            Width           =   1215
         End
         Begin VB.Label Label18 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Result    Errors      Polls"
            ForeColor       =   &H00000000&
            Height          =   195
            Index           =   1
            Left            =   180
            TabIndex        =   38
            Top             =   1440
            Width           =   2055
         End
      End
      Begin VB.PictureBox Picture1 
         Appearance      =   0  'Flat
         AutoSize        =   -1  'True
         BorderStyle     =   0  'None
         ForeColor       =   &H80000008&
         Height          =   1290
         Left            =   -65550
         Picture         =   "MiniHMI.frx":0AFF
         ScaleHeight     =   1290
         ScaleWidth      =   1800
         TabIndex        =   33
         ToolTipText     =   "Click here for ASDIRECT version information"
         Top             =   1560
         Width           =   1800
      End
      Begin MSFlexGridLib.MSFlexGrid GridWrite 
         Height          =   4275
         Left            =   -71820
         TabIndex        =   8
         Top             =   540
         Width           =   8655
         _ExtentX        =   15266
         _ExtentY        =   7541
         _Version        =   393216
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
      End
      Begin MSFlexGridLib.MSFlexGrid GridRead 
         Height          =   4275
         Left            =   -71820
         TabIndex        =   32
         Top             =   540
         Width           =   8655
         _ExtentX        =   15266
         _ExtentY        =   7541
         _Version        =   393216
         Redraw          =   -1  'True
         AllowBigSelection=   0   'False
         HighLight       =   0
         ScrollBars      =   0
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "Tahoma"
            Size            =   8.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
      End
      Begin VB.Frame Frame1 
         Caption         =   "Data"
         Height          =   3135
         Left            =   -74820
         TabIndex        =   26
         Top             =   1680
         Width           =   2835
         Begin VB.ComboBox optDataFormatRead 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            ItemData        =   "MiniHMI.frx":121A
            Left            =   120
            List            =   "MiniHMI.frx":122D
            Style           =   2  'Dropdown List
            TabIndex        =   2
            ToolTipText     =   "Select data format: word, float, long"
            Top             =   2310
            Width           =   2535
         End
         Begin VB.TextBox txtMemQtyRead 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   1
            Text            =   "1"
            ToolTipText     =   "Specify the number of V Memory elements to read."
            Top             =   1140
            Width           =   795
         End
         Begin VB.TextBox txtMemStartRead 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   0
            Text            =   "0"
            ToolTipText     =   "Specify starting V memory address."
            Top             =   540
            Width           =   1275
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   372
            Index           =   2
            Left            =   5340
            TabIndex        =   30
            Text            =   "11"
            Top             =   1380
            Width           =   972
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   372
            Index           =   5
            Left            =   5340
            TabIndex        =   29
            Text            =   "11"
            Top             =   1020
            Width           =   972
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   372
            Index           =   6
            Left            =   5340
            TabIndex        =   28
            Text            =   "1"
            Top             =   660
            Width           =   972
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   372
            Index           =   7
            Left            =   5340
            TabIndex        =   27
            Text            =   "1"
            Top             =   300
            Width           =   972
         End
         Begin VB.Label Label3 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Data Format"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   4
            Left            =   120
            TabIndex        =   379
            Top             =   2070
            Width           =   1050
         End
         Begin VB.Label Label3 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "V Memory Address (octal)"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   1
            Left            =   120
            TabIndex        =   41
            Top             =   300
            Width           =   2535
         End
         Begin VB.Label lblMemQtyRead 
            Appearance      =   0  'Flat
            Caption         =   "Quantity (16-bit words)"
            ForeColor       =   &H00C00000&
            Height          =   195
            Left            =   120
            TabIndex        =   40
            Top             =   900
            Width           =   2535
         End
         Begin VB.Label Label1 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            Caption         =   "MemAddr"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000C0&
            Height          =   255
            Index           =   3
            Left            =   5340
            TabIndex        =   31
            Top             =   0
            Width           =   975
         End
      End
      Begin VB.TextBox Text4 
         Height          =   5880
         Left            =   -74700
         Locked          =   -1  'True
         MultiLine       =   -1  'True
         TabIndex        =   25
         Text            =   "MiniHMI.frx":125C
         Top             =   720
         Width           =   8490
      End
      Begin VB.Frame Frame3 
         Caption         =   "Data"
         Height          =   3135
         Left            =   -74820
         TabIndex        =   19
         Top             =   1680
         Width           =   2835
         Begin VB.ComboBox optDataFormatWrite 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            ItemData        =   "MiniHMI.frx":1690
            Left            =   120
            List            =   "MiniHMI.frx":16A3
            Style           =   2  'Dropdown List
            TabIndex        =   6
            ToolTipText     =   "Select data format: word, float, long"
            Top             =   2310
            Width           =   2535
         End
         Begin VB.TextBox txtMemStartWrite 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   4
            Text            =   "0"
            ToolTipText     =   "Specify starting V memory address."
            Top             =   540
            Width           =   1275
         End
         Begin VB.TextBox txtMemQtyWrite 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   5
            Text            =   "1"
            ToolTipText     =   "Specify the number of V Memory elements to write."
            Top             =   1140
            Width           =   795
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   372
            Index           =   0
            Left            =   5340
            TabIndex        =   23
            Text            =   "1"
            Top             =   300
            Width           =   972
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H00000000&
            Height          =   372
            Index           =   1
            Left            =   5340
            TabIndex        =   22
            Text            =   "1"
            Top             =   660
            Width           =   972
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   372
            Index           =   3
            Left            =   5340
            TabIndex        =   21
            Text            =   "11"
            Top             =   1020
            Width           =   972
         End
         Begin VB.TextBox txt_mem_addr 
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   372
            Index           =   4
            Left            =   5340
            TabIndex        =   20
            Text            =   "11"
            Top             =   1380
            Width           =   972
         End
         Begin VB.Label Label3 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Data Format"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   6
            Left            =   120
            TabIndex        =   380
            Top             =   2070
            Width           =   1050
         End
         Begin VB.Label lblMemQtyWrite 
            Appearance      =   0  'Flat
            Caption         =   "Quantity (16-bit words)"
            ForeColor       =   &H00C00000&
            Height          =   195
            Left            =   120
            TabIndex        =   50
            Top             =   900
            Width           =   2535
         End
         Begin VB.Label Label3 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "V Memory Address (octal)"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   0
            Left            =   120
            TabIndex        =   49
            Top             =   300
            Width           =   2535
         End
         Begin VB.Label Label1 
            Alignment       =   2  'Center
            Appearance      =   0  'Flat
            BackColor       =   &H00C0C0C0&
            Caption         =   "MemAddr"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            ForeColor       =   &H000000C0&
            Height          =   255
            Index           =   0
            Left            =   5340
            TabIndex        =   24
            Top             =   0
            Width           =   975
         End
      End
      Begin VB.Frame Frame4 
         Caption         =   "Communications"
         Height          =   2115
         Left            =   -74820
         TabIndex        =   18
         Top             =   4920
         Width           =   2835
         Begin VB.CommandButton cmdDisconnectWrite 
            Caption         =   "Disconnect"
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   1620
            TabIndex        =   422
            Top             =   1080
            Width           =   1095
         End
         Begin VB.TextBox txtTimeoutTransWrite 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   411
            ToolTipText     =   "Enter the IP Address or network node name of the target host."
            Top             =   1080
            Width           =   735
         End
         Begin VB.TextBox txtErrorsWrite 
            BackColor       =   &H8000000F&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   840
            Locked          =   -1  'True
            TabIndex        =   45
            Text            =   "0"
            ToolTipText     =   "Transaction error counter"
            Top             =   1680
            Width           =   795
         End
         Begin VB.TextBox txtResultWrite 
            BackColor       =   &H8000000F&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            Locked          =   -1  'True
            TabIndex        =   44
            Text            =   "0"
            ToolTipText     =   "Result of last transaction"
            Top             =   1680
            Width           =   735
         End
         Begin VB.TextBox txtPollsWrite 
            BackColor       =   &H8000000F&
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   1620
            Locked          =   -1  'True
            TabIndex        =   43
            Text            =   "0"
            ToolTipText     =   "Transaction poll counter"
            Top             =   1680
            Width           =   1095
         End
         Begin VB.TextBox txtNodeAddressWrite 
            BeginProperty Font 
               Name            =   "Tahoma"
               Size            =   8.25
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   315
            Left            =   120
            TabIndex        =   7
            ToolTipText     =   "Enter the IP Address or network node name of the target host."
            Top             =   480
            Width           =   2595
         End
         Begin ASADTCPLib.Asadtcp AsadtcpWrite 
            Left            =   2100
            Top             =   180
            _Version        =   131073
            _ExtentX        =   609
            _ExtentY        =   556
            _StockProps     =   0
            Function        =   1
         End
         Begin VB.Label Label5 
            AutoSize        =   -1  'True
            Caption         =   "mSec"
            ForeColor       =   &H00C00000&
            Height          =   195
            Left            =   900
            TabIndex        =   413
            ToolTipText     =   "Enter poll rate for auto-polling in mSec"
            Top             =   1200
            Width           =   495
         End
         Begin VB.Label Label18 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Transaction timeout"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   9
            Left            =   180
            TabIndex        =   412
            Top             =   840
            Width           =   1755
         End
         Begin VB.Label Label18 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Result    Errors      Polls"
            ForeColor       =   &H00000000&
            Height          =   195
            Index           =   5
            Left            =   180
            TabIndex        =   47
            Top             =   1440
            Width           =   2055
         End
         Begin VB.Label Label18 
            Appearance      =   0  'Flat
            AutoSize        =   -1  'True
            Caption         =   "Node Address"
            ForeColor       =   &H00C00000&
            Height          =   195
            Index           =   4
            Left            =   180
            TabIndex        =   46
            Top             =   240
            Width           =   1215
         End
      End
   End
End
Attribute VB_Name = "frmMainASADTCP"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Long, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Long) As Long
Const SW_SHOWNORMAL = 1

Dim GridVal As String

' Handles transaction results
' Used by Sync and Async routines
Private Sub ReadHandler()
  Dim i As Integer, col As Integer, Row As Integer, nMemQty As Integer
  
  On Error GoTo ERROR_HANDLER
  
  txtPollsRead = Val(txtPollsRead) + 1
  txtResultRead = Hex(AsadtcpRead.Result)
  txtMessageRead = AsadtcpRead.ResultString
  GridRead.Redraw = False
  If AsadtcpRead.Result = 0 Then
    Select Case optDataFormatRead.ListIndex
      Case 0, 3:
        nMemQty = AsadtcpRead.MemQty
      Case 1, 2, 4:
        nMemQty = AsadtcpRead.MemQty \ 2
    End Select
    Row = 1
    col = 1
    Dim nRow, nCol, nIndex
    nIndex = 0
    For nRow = GridRead.FixedRows To GridRead.Rows - 1
      For nCol = GridRead.FixedCols To GridRead.Cols - 1
      If nIndex >= nMemQty Then
        GridRead.TextMatrix(nRow, nCol) = ""
      Else
        Select Case optDataFormatRead.ListIndex
          Case 0:
            GridRead.TextMatrix(nRow, nCol) = AsadtcpRead.DataWord(nIndex)
          Case 1:
            GridRead.TextMatrix(nRow, nCol) = AsadtcpRead.DataFloat(nIndex)
          Case 2:
            GridRead.TextMatrix(nRow, nCol) = AsadtcpRead.DataLong(nIndex)
          Case 3:
            GridRead.TextMatrix(nRow, nCol) = AsadtcpRead.DataWordBCD(nIndex)
          Case 4:
            GridRead.TextMatrix(nRow, nCol) = AsadtcpRead.DataLongBCD(nIndex)
        End Select
      End If
      nIndex = nIndex + 1
      'col = col + 1
      Next
    Next
  Else
    txtErrorsRead = txtErrorsRead + 1
  End If
  GridRead.Redraw = True
  Exit Sub
  
ERROR_HANDLER:
  txtMessageRead = "Error " & Err.Number & ", " & Err.Description
End Sub

Private Sub AsadtcpRead_Complete(ByVal Result As Integer)
  ' Handle transaction results
  ReadHandler
End Sub

' Handles transaction results
' Used by Sync and Async routines
Private Sub WriteHandler()
  txtPollsWrite = Val(txtPollsWrite) + 1
  txtResultWrite = Hex(AsadtcpWrite.Result)
  txtMessageWrite = AsadtcpWrite.ResultString
  If AsadtcpWrite.Result <> 0 Then
    txtErrorsWrite = txtErrorsWrite + 1
  End If
End Sub

Private Sub AsadtcpWrite_Complete(ByVal nResult As Integer)
  ' Handle transaction results
  WriteHandler
End Sub

' Handles transaction results
' Used by Sync and Async routines
Private Sub ReadDHandler()
  Dim nByte As Integer, nBit As Integer, nBitIndex As Integer, nLEDIndex
  
  On Error GoTo ERROR_HANDLER
  
  txtPollsReadD = Val(txtPollsReadD) + 1
  txtResultReadD = Hex(AsadtcpReadD.Result)
  txtMessageReadD = AsadtcpReadD.ResultString
  If AsadtcpReadD.Result = 0 Then
    For nByte = 0 To (LED1.Count / 8) - 1
      For nBit = 0 To 7
        nLEDIndex = (nByte * 8) + nBit
        If nByte >= AsadtcpReadD.MemQty Then
          'Make the LED transparent
          LED1(nLEDIndex).FillStyle = 1
        Else
          'Make the LED opaque
          LED1(nLEDIndex).FillStyle = 0
          'If bit set then make screen LED green. else make it red
          LED1(nLEDIndex).FillColor = IIf(AsadtcpReadD.GetDataBitM(nLEDIndex), &HFF00&, &HFF&)
        End If
      Next
    Next
  Else
    txtErrorsReadD = txtErrorsReadD + 1
  End If
  Exit Sub
  
ERROR_HANDLER:
  txtMessageReadD = "Error " & Err.Number & ", " & Err.Description
End Sub

Private Sub AsadtcpReadD_Complete(ByVal nResult As Integer)
  ' Handle the results of the transaction
  ReadDHandler
End Sub

' Handles transaction results
' Used by Sync and Async routines
Private Sub WriteDHandler()
  txtPollsWriteD.Text = Val(txtPollsWriteD.Text) + 1
  txtResultWriteD.Text = Hex(AsadtcpWriteD.Result)
  txtMessageWriteD.Text = AsadtcpWriteD.ResultString
  If AsadtcpWriteD.Result <> 0 Then
    txtErrorsWriteD.Text = Val(txtErrorsWriteD.Text) + 1
  End If
End Sub

Private Sub AsadtcpWriteD_Complete(ByVal nResult As Integer)
  ' Handle the results of the transaction
  WriteDHandler
End Sub

Private Sub chkAutoPollRead_Click()
  AsadtcpRead.AutoPollEnabled = (chkAutoPollRead.Value = 1)
  cmdAsyncRead.Enabled = (chkAutoPollRead.Value = 0)
  cmdSyncRead.Enabled = (chkAutoPollRead.Value = 0)
End Sub

Private Sub chkAutoPollReadD_Click()
  AsadtcpReadD.AutoPollEnabled = (chkAutoPollReadD = 1)
  cmdAsyncReadD.Enabled = (chkAutoPollReadD.Value = 0)
  cmdSyncReadD.Enabled = (chkAutoPollReadD.Value = 0)
End Sub

Private Sub SetReadParams()
  AsadtcpRead.Function = ASADTCP_FUNC_READ
  If Left(txtMemStartRead, 1) = "V" Then
    AsadtcpRead.MemStart = txtMemStartRead
  Else
    AsadtcpRead.MemStart = "V" & txtMemStartRead
  End If
  Select Case optDataFormatRead.ListIndex
    Case 0, 3:
      AsadtcpRead.MemQty = Val(txtMemQtyRead)
    Case 1, 2, 4:
      AsadtcpRead.MemQty = Val(txtMemQtyRead) * 2
  End Select
  ' Perform an asynchronous (non-blocking) transaction
  AsadtcpRead.TimeoutTrans = Val(txtTimeoutTransRead.Text)
End Sub

Private Sub cmdAsyncRead_Click()
  On Error GoTo ERROR_HANDLER
  ' If busy, exit
  If AsadtcpRead.Busy = True Then Exit Sub
  ' Set up control
  SetReadParams
  ' Perform an asynchronous (non-blocking) transaction
  AsadtcpRead.AsyncRefresh
  ' Complete event will fire when transaction has completed
  Exit Sub

ERROR_HANDLER:
  txtMessageReadD = "Error " & Err.Number & ", " & Err.Description
  ' Normal Pointer
  MousePointer = vbNormal
End Sub


Private Sub cmdAsyncReadD_Click()
  On Error GoTo ERROR_HANDLER
  ' If busy, exit
  If AsadtcpReadD.Busy = True Then Exit Sub
  ' Set up control
  SetReadDParams
  ' Perform an asynchronous (non-blocking) transaction
  AsadtcpReadD.AsyncRefresh
  ' Complete event will fire when transaction has completed
  Exit Sub

ERROR_HANDLER:
  txtMessageReadD = "Error " & Err.Number & ", " & Err.Description
  ' Normal Pointer
  MousePointer = vbNormal
End Sub

Private Sub cmdAsyncWrite_Click()
  On Error GoTo ERROR_HANDLER
  ' If busy, exit
  If AsadtcpWrite.Busy = True Then Exit Sub
  ' Set up the control
  SetWriteParams
  ' Perform an asynchronous (non-blocking) transaction
  AsadtcpWrite.AsyncRefresh
  ' Complete event will fire when transaction has completed
  Exit Sub
  
ERROR_HANDLER:
  txtMessageWrite = "Error " & Err.Number & ", " & Err.Description
End Sub

Private Sub cmdAsyncWriteD_Click()
  On Error GoTo ERROR_HANDLER
  ' If busy, exit
  If AsadtcpWriteD.Busy = True Then Exit Sub
  
  Dim i As Integer
  
  If (Left(txtMemStartWriteD, 1) >= "0") And (Left(txtMemStartWriteD, 1) <= "9") Then
    MsgBox "Memory type not specified, format is <Memory Type><Address>, example Y10", vbExclamation Or vbOKOnly
    Exit Sub
  End If
  
  'Clear out any values left in the data array from previous trans.
  For i = 0 To 119
    AsadtcpWriteD.DataWord(i) = 0
  Next

  AsadtcpWriteD.MemQty = Val(txtMemQtyWriteD)
  AsadtcpWriteD.MemStart = txtMemStartWriteD
  
  AsadtcpWriteD.Function = ASADTCP_FUNC_WRITE
  AsadtcpWriteD.NodeAddress = txtNodeAddressWriteD
  
  'Load new values into data array
  For i = 0 To Check1.Count - 1
    AsadtcpWriteD.DataBit(i) = Check1(i)
  Next
  ' Perform an asynchronous (non-blocking) transaction
  AsadtcpWriteD.AsyncRefresh
  Exit Sub
  
ERROR_HANDLER:
  txtMessageWriteD = "Error " & Err.Number & ", " & Err.Description
End Sub

Private Sub cmdDisconnectRead_Click()
  AsadtcpRead.Disconnect
End Sub

Private Sub cmdDisconnectReadD_Click()
  AsadtcpReadD.Disconnect
End Sub

Private Sub cmdDisconnectWrite_Click()
  AsadtcpWrite.Disconnect
End Sub

Private Sub cmdDisconnectWriteD_Click()
  AsadtcpWriteD.Disconnect
End Sub

Private Sub cmdSyncRead_Click()
  On Error GoTo ERROR_HANDLER
  ' If busy, exit
  If AsadtcpRead.Busy = True Then Exit Sub
  ' Show hourglass
  MousePointer = vbHourglass
  ' Set up control
  SetReadParams
  AsadtcpRead.SyncRefresh
  ' Handle transaction results
  ReadHandler
  ' Normal Pointer
  MousePointer = vbNormal
  Exit Sub

ERROR_HANDLER:
  txtMessageRead = "Error " & Err.Number & ", " & Err.Description
  ' Normal Pointer
  MousePointer = vbNormal
End Sub

Private Sub SetReadDParams()
  AsadtcpReadD.Function = ASADTCP_FUNC_READ
  If (Left(txtMemStartReadD, 1) >= "0") And (Left(txtMemStartReadD, 1) <= "9") Then
    MsgBox "Memory type not specified, format is <Memory Type><Address>, example Y10", vbExclamation Or vbOKOnly
    Exit Sub
  End If
  
  Dim sByte, nBit
  sByte = txtMemStartReadD
  'sByte = Left(txtMemStartReadD, Len(txtMemStartReadD) - 1) + "0"
  'nBit = Val(Right(txtMemStartReadD, 1))
  'Drop least significant digit, which specifies the bit
  AsadtcpReadD.MemStart = sByte
  AsadtcpReadD.MemQty = IIf(nBit = 0, Val(txtMemQtyReadD), Val(txtMemQtyReadD) + 1)
  AsadtcpReadD.TimeoutTrans = Val(txtTimeoutTransReadD.Text)
End Sub

Private Sub cmdSyncReadD_Click()
  On Error GoTo ERROR_HANDLER
  ' If busy, exit
  If AsadtcpReadD.Busy = True Then Exit Sub
  ' Show hourglass
  MousePointer = vbHourglass
  ' Set up control
  SetReadDParams
  ' Perform a synchronous (blocking) transaction
  AsadtcpReadD.SyncRefresh
  ' Handle transaction results
  ReadDHandler
  ' Normal Pointer
  MousePointer = vbNormal
  Exit Sub
  
ERROR_HANDLER:
  txtMessageReadD = "Error " & Err.Number & ", " & Err.Description
  ' Normal Pointer
  MousePointer = vbNormal
End Sub

Private Sub SetWriteParams()
  Dim i As Integer
 
  AsadtcpWrite.Function = ASADTCP_FUNC_WRITE
  If Left(txtMemStartWrite, 1) = "V" Then
    AsadtcpWrite.MemStart = txtMemStartWrite
  Else
    AsadtcpWrite.MemStart = "V" & txtMemStartWrite
  End If
  Select Case optDataFormatRead.ListIndex
    Case 0, 3:
      AsadtcpWrite.MemQty = Val(txtMemQtyWrite)
    Case 1, 2, 4:
      AsadtcpWrite.MemQty = Val(txtMemQtyWrite) * 2
  End Select
  For i = 0 To Val(txtMemQtyWrite) - 1
    GridWrite.Row = (i \ (GridWrite.Cols - 1)) + 1
    GridWrite.col = (i Mod (GridWrite.Cols - 1)) + 1
    'Load the values into the data array
      Select Case optDataFormatWrite.ListIndex
        Case 0:
          AsadtcpWrite.SetDataWordM i, CInt(Val(GridWrite.Text()))
        Case 1:
          AsadtcpWrite.SetDataFloatM i, CSng(Val(GridWrite.Text()))
        Case 2:
          AsadtcpWrite.SetDataLongM i, CLng(Val(GridWrite.Text()))
        Case 3:
          AsadtcpWrite.SetDataWordBCDM i, CInt(Val(GridWrite.Text()))
        Case 4:
          AsadtcpWrite.SetDataLongBCDM i, CLng(Val(GridWrite.Text()))
      End Select
  Next
  AsadtcpWrite.TimeoutTrans = Val(txtTimeoutTransWrite)
End Sub

Private Sub cmdSyncWrite_Click()
  On Error GoTo ERROR_HANDLER
  ' If busy, exit
  If AsadtcpWrite.Busy = True Then Exit Sub
  ' Show hourglass
  MousePointer = vbHourglass
  ' Set up the control
  SetWriteParams
  ' Perform a synchronous (blocking) transaction
  AsadtcpWrite.SyncRefresh
  ' Handle transaction results
  WriteHandler
  ' Normal Pointer
  MousePointer = vbNormal
  Exit Sub
  
ERROR_HANDLER:
  txtMessageWrite = "Error " & Err.Number & ", " & Err.Description
  ' Normal Pointer
  MousePointer = vbNormal
End Sub

Private Sub SetWriteDParams()
  Dim i As Integer
  
  If (Left(txtMemStartWriteD, 1) >= "0") And (Left(txtMemStartWriteD, 1) <= "9") Then
    MsgBox "Memory type not specified, format is <Memory Type><Address>, example Y10", vbExclamation Or vbOKOnly
    Exit Sub
  End If
  
  'Clear out any values left in the data array from previous trans.
  For i = 0 To 119
    AsadtcpWriteD.DataWord(i) = 0
  Next

  AsadtcpWriteD.MemQty = Val(txtMemQtyWriteD)
  AsadtcpWriteD.MemStart = txtMemStartWriteD
  
  AsadtcpWriteD.Function = ASADTCP_FUNC_WRITE
  AsadtcpWriteD.NodeAddress = txtNodeAddressWriteD
  
  'Load new values into data array
  For i = 0 To Check1.Count - 1
    AsadtcpWriteD.DataBit(i) = Check1(i)
  Next
  AsadtcpWriteD.TimeoutTrans = Val(txtTimeoutTransWriteD.Text)
End Sub

Private Sub cmdSyncWriteD_Click()
  On Error GoTo ERROR_HANDLER
  ' If busy, exit
  If AsadtcpWriteD.Busy = True Then Exit Sub
  ' Show hourglass
  MousePointer = vbHourglass
  ' Set up control
  SetWriteDParams
  ' Perform a synchronous (blocking) transaction
  AsadtcpWriteD.SyncRefresh
  ' Handle transaction results
  WriteDHandler
  ' Normal Pointer
  MousePointer = vbNormal
  Exit Sub
  
ERROR_HANDLER:
  txtMessageWriteD = "Error " & Err.Number & ", " & Err.Description
  ' Normal Pointer
  MousePointer = vbNormal
End Sub

Private Sub Command1_Click()

End Sub

Private Sub Form_Load()
  Dim i As Integer
      
  'Set to Welcome tab
  SSTab1.Tab = 0

  'Initalize Read tab
  AsadtcpRead.MemStart = "V0"
  AsadtcpRead.Function = ASADTCP_FUNC_READ
  txtMemStartRead.Text = AsadtcpRead.MemStart
  txtMemQtyRead.Text = AsadtcpRead.MemQty
  txtNodeAddressRead.Text = AsadtcpRead.NodeAddress
  txtAutoPollIntervalRead.Text = AsadtcpRead.AutoPollInterval
  txtTimeoutTransRead.Text = AsadtcpRead.TimeoutTrans
  GridRead.Rows = 9
  ConfigureGridCols GridRead, 9, 1, 975, 730, "+", 0, 1
  NumberGridRows GridRead, 0
  optDataFormatRead.ListIndex = 0

  'Initalize Write tab
  AsadtcpWrite.MemStart = "V0"
  AsadtcpWrite.Function = ASADTCP_FUNC_WRITE
  txtMemStartWrite.Text = AsadtcpWrite.MemStart
  txtMemQtyWrite.Text = AsadtcpWrite.MemQty
  txtNodeAddressWrite.Text = AsadtcpWrite.NodeAddress
  txtTimeoutTransWrite.Text = AsadtcpWrite.TimeoutTrans
  GridWrite.Rows = 9
  ConfigureGridCols GridWrite, 9, 1, 975, 730, "+", 0, 1
  NumberGridRows GridWrite, 1
  optDataFormatWrite.ListIndex = 0
   
  'Initalize Read Discrete tab
  AsadtcpReadD.MemStart = "Y0"
  AsadtcpReadD.Function = ASADTCP_FUNC_READ
  txtMemStartReadD.Text = AsadtcpReadD.MemStart
  txtMemQtyReadD.Text = AsadtcpReadD.MemQty
  txtNodeAddressReadD.Text = AsadtcpReadD.NodeAddress
  txtAutoPollIntervalReadD.Text = AsadtcpReadD.AutoPollInterval
  txtTimeoutTransReadD.Text = AsadtcpReadD.TimeoutTrans
  NumberLabels lblReadDiscretes, AsadtcpReadD.MemStart
  
  'Initalize WriteD tab
  AsadtcpWriteD.MemStart = "Y0"
  AsadtcpWriteD.Function = ASADTCP_FUNC_WRITE
  txtMemStartWriteD.Text = AsadtcpWriteD.MemStart
  txtMemQtyWriteD = AsadtcpWriteD.MemQty
  txtNodeAddressWriteD = AsadtcpWriteD.NodeAddress
  txtTimeoutTransWriteD.Text = AsadtcpWriteD.TimeoutTrans
  NumberLabels lblWriteDiscretes, AsadtcpWriteD.MemStart
End Sub


Private Sub GridWrite_KeyPress(KeyAscii As Integer)
  If KeyAscii >= 32 And KeyAscii <= 255 Then
    GridVal = GridVal & Chr(KeyAscii)
    GridWrite.Text = GridVal
  ElseIf KeyAscii = 8 And Len(GridVal) >= 1 Then
    GridVal = Left(GridVal, Len(GridVal) - 1)
    GridWrite.Text = GridVal
  End If
End Sub

Private Sub GridWrite_RowColChange()
  GridVal = ""
End Sub

Private Sub lblHelp_Click()
  ShellExecute Me.hwnd, "open", "Asadtcp.chm", "", "", SW_SHOWNORMAL
End Sub

Private Sub lblOrdering_Click()
  ShellExecute Me.hwnd, "open", "http://automatedsolutions.com/products/asadtcp.asp", "", "C:\\", SW_SHOWNORMAL
End Sub

Private Sub lblReadme_Click()
  ShellExecute Me.hwnd, "edit", "readme.txt", "", "", SW_SHOWNORMAL
End Sub

Private Sub lblVersion_Click()
  AsadtcpRead.AboutBox
End Sub

Private Sub lblWebsite_Click()
  ShellExecute Me.hwnd, "open", "http://www.automatedsolutions.com", "", "C:\\", SW_SHOWNORMAL
End Sub

Private Sub optDataFormatRead_Click()
  Select Case optDataFormatRead.ListIndex
    Case 0:
      ConfigureGridCols GridRead, 9, 1, 975, 730, "+", 0, 1
      AsadtcpRead.MemQty = Val(txtMemQtyRead.Text)
      lblMemQtyRead = "Quantity (16-bit words)"
    Case 1:
      ConfigureGridCols GridRead, 5, 1, 1900, 730, "+", 0, 2
      AsadtcpRead.MemQty = Val(txtMemQtyRead.Text) * 2
      lblMemQtyRead = "Quantity (32-bit floats)"
    Case 2:
      ConfigureGridCols GridRead, 5, 1, 1900, 730, "+", 0, 2
      AsadtcpRead.MemQty = Val(txtMemQtyRead.Text) * 2
      lblMemQtyRead = "Quantity (32-bit longs)"
    Case 3:
      ConfigureGridCols GridRead, 9, 1, 975, 730, "+", 0, 1
      AsadtcpRead.MemQty = Val(txtMemQtyRead.Text)
      lblMemQtyRead = "Quantity (16-bit BCDs)"
    Case 4:
      ConfigureGridCols GridRead, 5, 1, 1900, 730, "+", 0, 2
      AsadtcpRead.MemQty = Val(txtMemQtyRead.Text) * 2
      lblMemQtyRead = "Quantity (32-bit BCDs)"
  End Select
End Sub

Private Sub optDataFormatWrite_Click()
  Select Case optDataFormatWrite.ListIndex
    Case 0:
      ConfigureGridCols GridWrite, 9, 1, 975, 730, "+", 0, 1
      AsadtcpWrite.MemQty = Val(txtMemQtyWrite.Text)
      lblMemQtyWrite = "Quantity (16-bit words)"
    Case 1:
      ConfigureGridCols GridWrite, 5, 1, 1900, 730, "+", 0, 2
      AsadtcpWrite.MemQty = Val(txtMemQtyWrite.Text) * 2
      lblMemQtyWrite = "Quantity (32-bit floats)"
    Case 2:
      ConfigureGridCols GridWrite, 5, 1, 1900, 730, "+", 0, 2
      AsadtcpWrite.MemQty = Val(txtMemQtyWrite.Text) * 2
      lblMemQtyWrite = "Quantity (32-bit longs)"
    Case 3:
      ConfigureGridCols GridWrite, 9, 1, 975, 730, "+", 0, 1
      AsadtcpWrite.MemQty = Val(txtMemQtyWrite.Text)
      lblMemQtyWrite = "Quantity (16-bit BCDs)"
    Case 4:
      ConfigureGridCols GridWrite, 5, 1, 1900, 730, "+", 0, 2
      AsadtcpWrite.MemQty = Val(txtMemQtyWrite.Text) * 2
      lblMemQtyWrite = "Quantity (32-bit BCDs)"
  End Select
End Sub

Private Sub txtAutoPollIntervalRead_Change()
  AsadtcpRead.AutoPollInterval = Val(txtAutoPollIntervalRead.Text)
End Sub

Private Sub txtAutoPollIntervalReadD_Change()
  AsadtcpReadD.AutoPollInterval = Val(txtAutoPollIntervalReadD.Text)
End Sub


Private Sub txtMemQtyRead_Validate(Cancel As Boolean)
  If Val(txtMemQtyRead) < 1 Or Val(txtMemQtyRead) > 64 Then
    MsgBox "Memory quantity must be in range 1..64", vbExclamation Or vbOKOnly
    Cancel = True
    Exit Sub
  End If
  
  Select Case optDataFormatRead.ListIndex
    Case 0:
      AsadtcpRead.MemQty = Val(txtMemQtyRead)
    Case 1, 2:
      AsadtcpRead.MemQty = Val(txtMemQtyRead) * 2
  End Select
End Sub

Private Sub txtMemQtyReadD_Validate(Cancel As Boolean)
  If Val(txtMemQtyReadD.Text) < 1 Or Val(txtMemQtyReadD.Text) > 26 Then
    MsgBox "Memory quantity must be in range 1..26 for this example application", vbExclamation Or vbOKOnly
    Cancel = True
    Exit Sub
  End If
  
  AsadtcpReadD.MemQty = Val(txtMemQtyReadD.Text)
End Sub

Public Sub NumberGridRows(g As MSFlexGrid, sMemStart As Variant)
  Dim sOctalStartAddr
  sOctalStartAddr = "&O" & Mid(sMemStart, 2)
  Dim i
  For i = 0 To g.Rows - 2
    g.TextMatrix(i + 1, 0) = "V" & Format(Oct(Val(sOctalStartAddr) + (i * (g.Cols - 1))), "00000")
  Next
End Sub

Public Sub ConfigureGridCols(g As MSFlexGrid, nCols, nFixedCols, nColWidth, nFixedColWidth, sPrefix As Variant, nStart As Variant, nIncrement As Variant)
  Dim i
  g.Cols = nCols
  g.FixedCols = nFixedCols
  g.ColWidth(0) = nFixedColWidth
  For i = 0 To g.Cols - 2
    g.ColWidth(i + 1) = nColWidth
    g.TextMatrix(0, i + 1) = sPrefix & (nStart + (i * nIncrement))
  Next
End Sub

Public Sub NumberLabels(l As Object, sMemStart As Variant)
  Dim sOctalStartAddr, sMemType
  If (Len(sMemStart) > 2) Then
    If (Mid(sMemStart, 2) >= "0") And (Mid(sMemStart, 2) <= "9") Then
      sMemType = Left(sMemStart, 1)
      sOctalStartAddr = "&O" & Mid(sMemStart, 2, Len(sMemStart) - 2) & "0"
    Else
      sMemType = Left(sMemStart, 2)
      sOctalStartAddr = "&O" & Mid(sMemStart, 3, Len(sMemStart) - 3) & "0"
    End If
  ElseIf (Len(sMemStart) > 1) Then
      sMemType = Left(sMemStart, 1)
      sOctalStartAddr = "&O" & Mid(sMemStart, 2, Len(sMemStart) - 2) & "0"
  End If
  Dim i
  For i = 0 To l.Count - 1
    l(i).Caption = sMemType & Oct((Val(sOctalStartAddr)) + (i * 8))
  Next
End Sub

Private Sub txtMemQtyWrite_Validate(Cancel As Boolean)
  If Val(txtMemQtyWrite) < 1 Or Val(txtMemQtyWrite) > 64 Then
    MsgBox "Memory quantity must be in range 1..64", vbExclamation Or vbOKOnly
    Cancel = True
    Exit Sub
  End If
  
  Select Case optDataFormatWrite.ListIndex
    Case 0:
      AsadtcpWrite.MemQty = Val(txtMemQtyWrite)
    Case 1, 2:
      AsadtcpWrite.MemQty = Val(txtMemQtyWrite) * 2
  End Select
End Sub

Private Sub txtMemQtyWriteD_Validate(Cancel As Boolean)
  If Val(txtMemQtyWriteD.Text) < 1 Or Val(txtMemQtyWriteD.Text) > 26 Then
    MsgBox "Memory quantity must be in range 1..26 for this example application", vbExclamation Or vbOKOnly
    Cancel = True
    Exit Sub
  End If
  
  AsadtcpWriteD.MemQty = Val(txtMemQtyWriteD.Text)
End Sub

Private Sub txtMemStartRead_Validate(Cancel As Boolean)
  On Error GoTo ERROR_HANDLER
  
  Dim sMemStartRead
  'Only V types allowed here, so prepend a 'V' if not present
  If Left(txtMemStartRead.Text, 1) = "V" Then
    sMemStartRead = txtMemStartRead.Text
  ElseIf (Left(txtMemStartRead.Text, 1) >= "0") And (Left(txtMemStartRead.Text, 1) <= "9") Then
    sMemStartRead = "V" & txtMemStartRead.Text
  Else
    MsgBox "Only V memory adddresses allowed for Read V Memory tab."
    Cancel = True
    Exit Sub
  End If
  'MemStart will throw an exception on attempt to set invalid address
  AsadtcpRead.MemStart = sMemStartRead
  txtMemStartRead.Text = sMemStartRead
  NumberGridRows GridRead, sMemStartRead
  Exit Sub

ERROR_HANDLER:
  MsgBox Err.Description
  Cancel = True
End Sub

Private Sub txtMemStartReadD_Validate(Cancel As Boolean)
  On Error GoTo ERROR_HANDLER
  
  'MemStart will throw an exception on attempt to set invalid address
  AsadtcpReadD.MemStart = txtMemStartReadD.Text
  NumberLabels lblReadDiscretes, txtMemStartReadD.Text
  Exit Sub

ERROR_HANDLER:
  MsgBox Err.Description
  Cancel = True
End Sub

Private Sub txtMemStartWrite_Validate(Cancel As Boolean)
  On Error GoTo ERROR_HANDLER
  
  Dim sMemStartWrite
  'Only V types allowed here, so prepend a 'V' if not present
  If Left(txtMemStartWrite.Text, 1) = "V" Then
    sMemStartWrite = txtMemStartWrite.Text
  ElseIf (Left(txtMemStartWrite.Text, 1) >= "0") And (Left(txtMemStartWrite.Text, 1) <= "9") Then
    sMemStartWrite = "V" & txtMemStartWrite.Text
  Else
    MsgBox "Only V memory adddresses allowed for Write V Memory tab."
    Cancel = True
    Exit Sub
  End If
  'MemStart will throw an exception on attempt to set invalid address
  AsadtcpWrite.MemStart = sMemStartWrite
  txtMemStartWrite.Text = sMemStartWrite
  NumberGridRows GridWrite, sMemStartWrite
  Exit Sub

ERROR_HANDLER:
  MsgBox Err.Description
  Cancel = True
End Sub

Private Sub txtMemStartWriteD_Validate(Cancel As Boolean)
  On Error GoTo ERROR_HANDLER
  
  'MemStart will throw an exception on attempt to set invalid address
  AsadtcpWriteD.MemStart = txtMemStartWriteD.Text
  NumberLabels lblWriteDiscretes, txtMemStartWriteD.Text
  Exit Sub

ERROR_HANDLER:
  MsgBox Err.Description
  Cancel = True
End Sub

Private Sub txtNodeAddressRead_Change()
  AsadtcpRead.NodeAddress = txtNodeAddressRead.Text
End Sub

Private Sub txtNodeAddressWrite_Change()
  AsadtcpWrite.NodeAddress = txtNodeAddressWrite.Text
End Sub

Private Sub txtNodeAddressReadD_Change()
  AsadtcpReadD.NodeAddress = txtNodeAddressReadD.Text
End Sub

Private Sub txtNodeAddressWriteD_Change()
  AsadtcpWriteD.NodeAddress = txtNodeAddressWriteD.Text
End Sub

