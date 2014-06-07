MODULE MainModule
    
    PROC main()
        MoveAbsJ [[0, 0, 55, 0, -66, 0],[9E9,9E9,9E9,9E9,9E9,9E9]], v200, fine, tool0;
		MoveL [[%XCoord%,%YCoord%, 170],[0.77, 0.0, 0.636, 0.0],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]], v1000, fine, tool0;
		Set DO10_6;
        MoveL [[%XCoord%,%YCoord%, 133.8],[0.77, 0.0, 0.636, 0.0],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]], v100, fine, tool0;
        WaitTime 0.5;
        MoveAbsJ [[0, 0, 55, 0, -66, 0],[9E9,9E9,9E9,9E9,9E9,9E9]], v1000, fine, tool0;
        MoveL [[285.7, -268.6, 366.5],[0.77, 0.0, 0.636, 0.0],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]], v1000, fine, tool0;
        %MagazineHover%
        %MagazineDrop%
        WaitTime 0.5;
        Reset DO10_6;
        WaitTime 0.5;
        MoveL [[285.7, -268.6, 366.5],[0.77, 0.0, 0.636, 0.0],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]], v1000, fine, tool0;
        MoveAbsJ [[0, 0, 55, 0, -66, 0],[9E9,9E9,9E9,9E9,9E9,9E9]], v1000, fine, tool0;
    ENDPROC
ENDMODULE