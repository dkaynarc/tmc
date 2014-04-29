package services;


import java.io.BufferedInputStream;
import java.net.HttpURLConnection;
import java.net.URL;

import model.Constants;
import android.app.IntentService;
import android.content.Intent;
import android.os.Bundle;


public class SynchService extends IntentService 
{
    private String urlString = "http://192.168.1.5:9000/api/Server/";
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
	    	  placeNewOrder(parcel);
	          break;
	      
	      case 3:
	    	  break;
	     
	      case 4:
	    	  break;
	     
	      default:
	    	break;
	    
	    }
		  
	}

	



	
	
private void placeNewOrder(Bundle parcel) 
{
    urlString +=  "PlaceOrder/" + parcel.getString("orderName") + "/" + parcel.getString("itemsNumber"); 
    
    String response =  connect(urlString);	
    
    notifyCaller(response);
	

	
	
}


private void authenticate(Bundle parcel) 
{
    urlString +=  "Authenticate/" + parcel.getString("userName") + "/" + parcel.getString("password"); 
    
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
	   System.out.print("Exception: " + exc.toString());
	 }	
     
     return str.toString();
}









private void notifyCaller(String response) 
{
	   Intent intent = new Intent();
	   intent.setAction(Constants.FEEDBACK);
	   intent.putExtra("command", Integer.toString(command));
	   intent.putExtra("result", response);	   
	   sendBroadcast(intent);
}

}


