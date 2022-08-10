using UnityEngine;

public class BuildSystem : MonoBehaviour
{
	public GameObject[] Blocks;
	public GameObject[] Previews;

	GameObject Block;
	GameObject previewBlock;

	int rotY = 0;
	Vector3 spawnRot;

	private void Start()
	{
		Block = Blocks[0];
		previewBlock = Previews[0];
	}

	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		spawnRot.y = rotY;

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SelectBlock(0);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SelectBlock(1);
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SelectBlock(2);
		}

		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			SelectBlock(3);
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			rotY -= 90;
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			rotY += 90;
		}

		if (Physics.Raycast(ray, out hit, 5))
		{
			if (hit.transform.tag == "Buildable")
			{
				Vector3 prewPos = hit.point;
				previewBlock.transform.position = new Vector3(Mathf.Round(prewPos.x), Mathf.Round(prewPos.y), Mathf.Round(prewPos.z));
				previewBlock.transform.localEulerAngles = spawnRot;

				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					GameObject Build = Instantiate(Block, transform.position, transform.rotation);
					Vector3 buildPos = hit.point;

					Build.transform.localEulerAngles = spawnRot;
					Build.transform.position = new Vector3(Mathf.Round(buildPos.x), Mathf.Round(buildPos.y), Mathf.Round(buildPos.z));
				}

				if (Input.GetKeyDown(KeyCode.Mouse1))
				{
					Destroy(hit.transform.gameObject);
				}

			}			
		}

	}

	public void SelectBlock(int selection)
	{
		previewBlock.SetActive(false);

		Block = Blocks[selection];
		previewBlock = Previews[selection];

		previewBlock.SetActive(true);
	}
}
