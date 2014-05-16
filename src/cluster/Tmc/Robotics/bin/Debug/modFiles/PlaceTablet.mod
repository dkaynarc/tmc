MODULE MainModule
    PROC main()
        %MagazineHover%
        %Tablet%
        Set DO10_6;
        WaitTime 0.5;
        %MagazineHover%
        %TabletDrop%
		WaitTime 0.5;
        Reset DO10_6;
        WaitTime 0.5;
        MoveL [[165.6,-499.9,357.6],[0.53770,0.47561,0.50054,-0.48387],[0,0,-1,1],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]],v500,fine,tool0;
    ENDPROC
ENDMODULE