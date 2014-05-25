MODULE MainModule
    CONST jointtarget hoveringpoint1:=[[-91.1, 2.8, 21.7, -12, -27.6, 10.8],[9E9,9E9,9E9,9E9,9E9,9E9]];         
	CONST jointtarget touchingtablet1:=[[-92.1, 29.6, 37.7, -8.0, -70.1, 3.3],[9E9,9E9,9E9,9E9,9E9,9E9]];
    
    
    CONST jointtarget touchingtablet2:=[[-91.1,34.2,27.7,-6.2,-64.7,2.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget touchingtablet3:=[[-91.1,47.7,4.0,-6.3,-54.5,3.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget touchingtablet4:=[[-92.4,56,-11.1,-9.3,-47.9,10.3],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget touchingtablet5:=[[-102.9,29.9,35,-18.6,-68.7,7.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget touchingtablet6:=[[-101.5,36.9,25.1,-17.5,-65.7,8.0],[9E9,9E9,9E9,9E9,9E9,9E9]];    
    CONST jointtarget touchingtablet7:=[[-99.6,49.4,1.8,-17.2,-55.1,10.6],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget touchingtablet8:=[[-98.8,57,-11.8,-17.6,-49.2,12.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget touchingtablet9:=[[-68 ,55.4,-7.7,22.3,-52.6,-12.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
    
    CONST jointtarget middletray:=[[-68, 38.6, -20.7, 42, -26.8, -36.9],[9E9,9E9,9E9,9E9,9E9,9E9]];
    
    CONST jointtarget tray1:=[[-76.3, 42.0, 21.0, 10, -67.0, -1.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray2:=[[-66.4, 42.5, 16.2, 21.4, -63.1, -8.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray3:=[[-56.5,48.6, 5.0, 33.4, -61.1, -16.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray4:=[[-61.3, 60.6, -17.4, 31.9, -50.6, -19.9],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray5:=[[-64.9, 80.0, -49.7, 34.5, -38.4, -26.6],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray6:=[[-72.4, 71.5, -36.5, 20.9, -38.7, -17.5],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray7:=[[-79.4, 67.2, -28.4, 9.2, -41.0, -4.6],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray8:=[[-77.5, 54.2, -0.2, 9.6, -57.0, -2.9],[9E9,9E9,9E9,9E9,9E9,9E9]];
    
    PROC main()
      MoveAbsJ hoveringpoint1, v200, fine, tool0;
      MoveAbsJ touchingtablet3, v200, fine, tool0;
      WaitTime (0.5);
      Set DO10_6;
      MoveAbsJ hoveringpoint1, v200, fine, tool0;
      MoveAbsJ middletray, v200, fine, tool0;
      MoveAbsJ tray5, v200, fine, tool0;
      WaitTime (0.5);
      Reset DO10_6;
      
      !MoveAbsJ touchingtablet5, v50, fine, tool0;
      !Set DO10_6;
      !MoveAbsJ midpoint, v200,fine, tool0;
      !Reset DO10_6;
           
      !MoveAbsJ touchingtablet6, v50, fine, tool0;
      !Set DO10_6;
      !MoveAbsJ midpoint, v200,fine, tool0;
      !Reset DO10_6;
      
      !MoveAbsJ touchingtablet7, v50, fine, tool0;
      !Set DO10_6;
      !MoveAbsJ midpoint, v200,fine, tool0;
      !Reset DO10_6;
           
      !MoveAbsJ touchingtablet8, v50, fine, tool0;
      !Set DO10_6;
      !MoveAbsJ midpoint, v200,fine, tool0;
      !Reset DO10_6;             

    ENDPROC
ENDMODULE