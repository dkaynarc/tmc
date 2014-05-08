MODULE Module1
	
    
    VAR string str;
    
	CONST robtarget Target_set:=[[-77.7,-389.7,307.2],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Target_Reset:=[[165.6,-499.9,357.6],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    
    CONST robtarget Target_1:=[[-77.7,-389.7,244.8],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Target_2:=[[-77.7,-389.7,242.4],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Target_3:=[[-77.7,-389.7,239.9],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Target_4:=[[-77.7,-389.7,237.4],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Target_5:=[[-77.7,-389.7,235.0],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Target_6:=[[-77.7,-389.7,232.6],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Target_7:=[[-77.7,-389.7,230.1],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Target_8:=[[-77.7,-389.7,227.7],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Target_9:=[[-77.7,-389.7,225.2],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    CONST robtarget Target_10:=[[-77.7,-389.7,222.8],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    
    CONST jointtarget tray1:=[[-76.3, 42.0, 21.0, 10, -67.0, -1.7],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray2:=[[-66.4, 42.5, 16.2, 21.4, -63.1, -8.2],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray3:=[[-56.5,48.6, 5.0, 33.4, -61.1, -16.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray4:=[[-61.3, 60.6, -17.4, 31.9, -50.6, -19.9],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray5:=[[-64.9, 80.0, -49.7, 34.5, -38.4, -26.6],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray6:=[[-72.4, 71.5, -36.5, 20.9, -38.7, -17.5],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray7:=[[-79.4, 67.2, -28.4, 9.2, -41.0, -4.6],[9E9,9E9,9E9,9E9,9E9,9E9]];
    CONST jointtarget tray8:=[[-77.5, 54.2, -0.2, 9.6, -57.0, -2.9],[9E9,9E9,9E9,9E9,9E9,9E9]];
    
PROC Path_10(robtarget target )
    
	MoveL Target_set,v200,fine,tool0;
    MoveL Target,v200,fine,tool0;
	Set DO10_6;
    WaitTime 0.5;
	MoveL Target_set,v200,fine,tool0;
ENDPROC

PROC Path_20(jointtarget Jtarget)
    MoveAbsJ Jtarget,v200,fine,tool0;
    Reset DO10_6;
    WaitTime 0.5;
    MoveL Target_Reset,v200,fine,tool0;
    
ENDPROC  
PROC main()
    FOR i FROM 1 TO 10  DO
      IF i = 1 THEN
          Path_10(target_1);
           Path_20(tray1);
      ELSEIF i = 2 THEN
          Path_10(target_2);
           Path_20(tray2);
      ELSEIF i = 3 THEN
          Path_10(target_3);
           Path_20(tray3);
      ELSEIF i = 4 THEN
          Path_10(target_4);
           Path_20(tray4);
     ELSEIF i = 5 THEN
         Path_10(target_5);
           Path_20(tray5);
      ELSEIF i = 6 THEN
          Path_10(target_6);
           Path_20(tray6);
       ELSEIF i = 7 THEN
           Path_10(target_7);
           Path_20(tray7);
       ELSEIF i = 8 THEN
           Path_10(target_8);
           Path_20(tray8);
      ENDIF
    ENDFOR
    
ENDPROC

ENDMODULE