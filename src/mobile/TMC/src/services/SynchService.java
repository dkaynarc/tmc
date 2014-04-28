package services;


import java.io.BufferedInputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import android.app.IntentService;
import android.content.Intent;
import android.os.Bundle;


public class SynchService extends IntentService 
{
	public static final String FEEDBACK = "FEEDBACK";


    private String urlString = "http://192.168.1.3:9000/api/Server/";
    private int command;

    public SynchService()
    {
		super("SynchService");
	}
    

	@Override
	protected void onHandleIntent(Intent intent) 
	{
		Bundle parcel = intent.getBundleExtra("parcel");
	   command = Integer.decode(parcel.getString("command"));

	    switch(command)
	    {
	      case 1:
	    	  authenticate(parcel);
	          break;
	      case 2:
	          break;
	      case 3:
	    	  break;
	      case 4:
	    	  break;
	      default:
	    	break;
	    
	    }
		  
	}

	



	
	
private void authenticate(Bundle parcel) 
{
    String userName = parcel.getString("userName");
    String password = parcel.getString("password");   
    urlString +=  "Authenticate/" + userName + "/" + password; 
    
    String response =  connect(urlString);	
    
    notifyCaller(response);
}






    private String connect(String urlStr) 
	{
    	StringBuilder str = new StringBuilder();
     
    	try{
		  
		  URL url = new URL(urlStr);
		  HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();
		  urlConnection.setRequestMethod("GET");
	      BufferedInputStream in = new BufferedInputStream(urlConnection.getInputStream());
   
	      // read result
		  byte[] contents = new byte[1024];
		  int flag;
		 
	      while((flag = in.read(contents)) !=-1 )
		  {
		    str.append(new String(contents,0,flag));
		  }	
		
	    }
	 catch(Exception exc)
	 {
	   String excStr = exc.toString();	    
	 }	
     
     return str.toString();
}









private void notifyCaller(String response) 
{
	   Intent intent = new Intent();
	   intent.setAction(FEEDBACK);
	   intent.putExtra("command", Integer.toString(command));
	   intent.putExtra("result", response);	   
	   sendBroadcast(intent);
}

}


