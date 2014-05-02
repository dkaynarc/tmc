package services;



import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import model.Constants;
import android.app.IntentService;
import android.content.Intent;
import android.os.Bundle;


public class SynchService extends IntentService 
{
    private String urlString = "http://192.168.1.5:9000/api/Server/";
    //private String urlString = "http://172.19.14.150:9000/api/Server/";    
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
	      
	    case 1://AUTHENTICATE
	    	  authenticate(parcel);
	          break;
	      
	      case 2://NEW_ORDER
	    	  placeNewOrder(parcel);
	          break;
	      
	      case 3:
	    	  getOrdersUpdate();
	    	  break;
	     
	      case 4:
	    	  break;
	     
	      default:
	    	break;
	    
	    }
		  
	}

	



	
	
private void getOrdersUpdate()
{
    urlString +=  "GetOrders" ; 
    
    String response =  connect(urlString);	
    
    notifyCaller(response);
	
}



private void placeNewOrder(Bundle parcel) 
{
    urlString +=  "PlaceOrder/" + parcel.getString("orderName") + "/" 
          + parcel.getString("black")+ "/" 
    	  + parcel.getString("blue")+ "/" 
          + parcel.getString("green")+ "/" 
    	  + parcel.getString("red")+ "/" 
          + parcel.getString("white"); 
    
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
		  HttpURLConnection con = (HttpURLConnection) url.openConnection();
		  con.setRequestMethod("GET");
		  con.setConnectTimeout(100000); // times out after 10 seconds
		  //con.setRequestProperty("Content-Type","application/json");
		  con.setRequestProperty("Accept", "application/json");
	      BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
   

		  String line;
		 
	      while((line = in.readLine()) !=null )
		  {
		    str.append(line);
		  }	
		  in.close();
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
	
	
	switch(command)
	{
	case 1:
		intent.setAction(Constants.AUTHENTICATE_RESULT);
		break;
		
		case 2:
			intent.setAction(Constants.NEW_ORDER_RESULT);
			break;
	
		case 3:
			intent.setAction(Constants.ORDER_UPDATE_RESULT);
			break;
	}

	
	intent.putExtra("command", Integer.toString(command));
	intent.putExtra("result", response);	   
	sendBroadcast(intent);
}

}


