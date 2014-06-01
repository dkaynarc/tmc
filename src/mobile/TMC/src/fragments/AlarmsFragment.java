/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package fragments;

import java.util.ArrayList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import model.Alarm;
import model.Constants;
import ictd.activities.R;
import adapters.AlarmsAdapter;
import android.app.ProgressDialog;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.support.v4.app.ListFragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;


public class AlarmsFragment extends ListFragment
{
	private ProgressDialog pd;
	private ResultReceiver receiver;




	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState)
	{
		View rootView = inflater.inflate(R.layout.list_alarms, container, false);

		setListAdapter(new AlarmsAdapter(getActivity(), R.layout.alarm_row, new ArrayList<Alarm>()));

		return rootView;
	}

	
	
	public void onStart()
	{
		receiver = new ResultReceiver();
		getActivity().registerReceiver(	receiver, new IntentFilter(Integer.toString(Constants.GET_ALARM_COMMAND)));
		makeService();
		super.onStart();
	}

	
	@Override
	public void onStop()
	{
		getActivity().unregisterReceiver(receiver);
		super.onStop();
	}
	
	
	
	private void makeService()
	{
		Intent service = new Intent(getActivity(), services.SynchService.class);
		Bundle parcel = new Bundle();
		parcel.putInt("command", Constants.GET_ALARM_COMMAND);
		service.putExtra("parcel", parcel);

		pd = ProgressDialog.show(getActivity(), null, "Contacting server");
		getActivity().startService(service);
	}
	
	
	// private class
		private class ResultReceiver extends BroadcastReceiver
		{
			@Override
			public void onReceive(Context context, Intent intent)
			{
				pd.dismiss();
				String response = intent.getStringExtra("result");
				handleAlarms(response);
			}
		}

		
		
		
		
		
		private void handleAlarms(String response) 
		{
			Log.v("MAD", response);

			AlarmsAdapter adapter = (AlarmsAdapter) getListView() .getAdapter();
			adapter.clear();

			ArrayList<Alarm> alarms = new ArrayList<Alarm>();
			JSONArray jArray;
			try
			{
				jArray = new JSONArray(response);

				for (int i = 0; i < jArray.length(); i++)
				{
					JSONObject jObj = jArray.getJSONObject(i);

					Alarm alarm = new Alarm(
							jObj.getString("Id"),
							jObj.getString("Type"), 
							jObj.getString("Description"),
							jObj.getString("Time"));

					alarms.add(alarm);
				}
			}
			catch (JSONException e)
			{
				Log.v("MAD", e.toString());
			}

			adapter.addAll(alarms);
			adapter.notifyDataSetChanged();			
		}
		
	
		
}
