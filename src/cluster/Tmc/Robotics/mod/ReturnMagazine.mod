MODULE MainModule
   CONST jointtarget central:=[[0, 0, 55, 0, -66, 0],[9E9,9E9,9E9,9E9,9E9,9E9]];
   CONST jointtarget above:=[[-66, 40.8, -17.1, -70.0, -75.7, 52],[9E9,9E9,9E9,9E9,9E9,9E9]];
   CONST jointtarget above2:=[[-87.6, 14.1, 45.9, -83.0, -99.4, 27.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
   CONST jointtarget touching:=[[-66.4, 58.1, -13, -65.8, -82.9, 38.4],[9E9,9E9,9E9,9E9,9E9,9E9]];
   CONST jointtarget touching2:=[[-87.6, 47.8, 40.8, -88.4, -101.6, -1.6],[9E9,9E9,9E9,9E9,9E9,9E9]];
   CONST jointtarget return1:=[[-66.9, 63.2, -25.8, -66.9, -79.0, 48.1],[9E9,9E9,9E9,9E9,9E9,9E9]];
    PROC main()
        MoveAbsJ central, v200, fine, tool0;
        MoveAbsJ above2, v200, fine, tool0;
        MoveAbsJ touching2, v200, fine, tool0;
        Set DO10_6;
        MoveAbsJ above2, v200, fine, tool0;
        MoveAbsJ above, v200, fine, tool0;
        MoveAbsJ return1, v200, fine, tool0;
        MoveAbsJ touching, v200, fine, tool0;
        Reset DO10_6;
        MoveAbsJ above, v200, fine, tool0;
        MoveAbsJ central, v200, fine, tool0;
    ENDPROC
ENDMODULE