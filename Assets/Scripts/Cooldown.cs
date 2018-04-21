using System.Diagnostics;

public class Cooldown {
	private readonly long cooldownDurationMillis;
	private readonly Stopwatch stopwatch;
	private readonly bool initialReady;

	public Cooldown(long cooldownDurationMillis, bool initialReady = true) {
		this.cooldownDurationMillis = cooldownDurationMillis;
		this.stopwatch = new Stopwatch();
		this.initialReady = initialReady;
	}

	public bool IsReady() {
		bool ready = (initialReady ? stopwatch.ElapsedMilliseconds == 0 : false) || cooldownDurationMillis <= stopwatch.ElapsedMilliseconds;
		if (ready) {
			stopwatch.Stop();
			stopwatch.Reset();
		}
		return ready;
	}

	public void Trigger() {
		stopwatch.Stop();
		stopwatch.Reset();
		stopwatch.Start();
	}
}
