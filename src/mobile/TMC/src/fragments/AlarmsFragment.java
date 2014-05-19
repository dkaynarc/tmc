/* COPYRIGHT (C) 2014 Carlo Chumroonridhi. All Rights Reserved. */

package fragments;

import ictd.activities.R;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
//import android.widget.TextView;

public class AlarmsFragment extends Fragment
{
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState)
	{
		View rootView = inflater.inflate(R.layout.fragment_alarms, container,
				false);

//		TextView errors = (TextView) rootView
//				.findViewById(R.id.alarms_errors_tv);
//		TextView warnings = (TextView) rootView
//				.findViewById(R.id.alarms_warnings_tv);
//		TextView alarms = (TextView) rootView
//				.findViewById(R.id.alarms_alarms_tv);

		return rootView;
	}

}
