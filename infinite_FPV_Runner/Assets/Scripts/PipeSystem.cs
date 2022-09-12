﻿using UnityEngine;

public class PipeSystem : MonoBehaviour {

	public Pipe pipePrefab;

	public int pipeCount;

	public int emptyPipeCount;

	private Pipe[] pipes;

	private void Awake () {
		pipes = new Pipe[pipeCount];
		for (int i = 0; i < pipes.Length; i++) {
			Pipe pipe = pipes[i] = Instantiate<Pipe>(pipePrefab);
			pipe.transform.SetParent(transform, false);
		}
	}

	public Pipe SetupFirstPipe () {
		for (int i = 0; i < pipes.Length; i++) {
			Pipe pipe = pipes[i];
			pipe.Generate(i > emptyPipeCount);
			if (i > 0) {
				pipe.AlignWith(pipes[i - 1]);
			}
		}
		AlignNextPipeWithOrigin();
		transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
		return pipes[1];
	}

	public Pipe SetupNextPipe () {
		ShiftPipes();
		AlignNextPipeWithOrigin();
		pipes[pipes.Length - 1].Generate();
		pipes[pipes.Length - 1].AlignWith(pipes[pipes.Length - 2]);
		transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
		return pipes[1];
	}

	private void ShiftPipes () {
		Pipe temp = pipes[0];
		for (int i = 1; i < pipes.Length; i++) {
			pipes[i - 1] = pipes[i];
		}
		pipes[pipes.Length - 1] = temp;
	}

	private void AlignNextPipeWithOrigin () {
		Transform transformToAlign = pipes[1].transform;
		for (int i = 0; i < pipes.Length; i++) {
			if (i != 1) {
				pipes[i].transform.SetParent(transformToAlign);
			}
		}
		
		transformToAlign.localPosition = Vector3.zero;
		transformToAlign.localRotation = Quaternion.identity;
		
		for (int i = 0; i < pipes.Length; i++) {
			if (i != 1) {
				pipes[i].transform.SetParent(transform);
			}
		}
	}
}