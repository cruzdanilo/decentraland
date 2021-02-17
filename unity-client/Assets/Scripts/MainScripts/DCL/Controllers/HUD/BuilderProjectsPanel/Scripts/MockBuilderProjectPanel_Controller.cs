﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal static class MockBuilderProjectPanel_Controller
{
    private static bool initialized = false;
    private static BuilderProjectsPanelController controller;

    public static void Initialize(BuilderProjectsPanelView view)
    {
        if (initialized) return;
        initialized = true;

        controller = new BuilderProjectsPanelController(view);
    }

    public static void AddScenes(int deployedCount, int projectCount)
    {
        if (!initialized)
        {
            Debug.LogError("Mock not initialized");
            return;
        }
        List<ISceneData> scenes = new List<ISceneData>();

        for (int i = 0; i < deployedCount; i++)
        {
            int role = Random.Range(0, 3);
            scenes.Add(new SceneData()
            {
                id = $"MyDeployedScene{i}",
                name = $"MyDeployedScene{i}",
                isDeployed = true,
                isOwner = role == 0,
                isOperator = role == 1,
                isContributor = role == 2,
                size = new Vector2Int(Random.Range(1,6),Random.Range(1,6)),
                coords = new Vector2Int(Random.Range(-100,100),Random.Range(-100,100)),
            });
        }
        for (int i = 0; i < projectCount; i++)
        {
            int role = Random.Range(0, 2);
            scenes.Add(new SceneData()
            {
                id = $"MyProject{i}",
                name = $"MyProject{i}",
                isDeployed = false,
                isOwner = role == 0,
                isContributor = role == 1,
                size = new Vector2Int(Random.Range(1,6),Random.Range(1,6)),
                coords = new Vector2Int(Random.Range(-100,100),Random.Range(-100,100)),
            });
        }
        controller.scenesViewController.SetScenes(scenes);
    }

    public static void RunSectionScenesTestSequence()
    {
        if (!initialized)
        {
            Debug.LogError("Mock not initialized");
            return;
        }
        RunSequence(SectionSceneTestSequence());
    }

    private static void RunSequence(IEnumerator coroutine)
    {
        controller.view.StartCoroutine(coroutine);
    }

    private static IEnumerator SectionSceneTestSequence()
    {
        const float TIME = 2;
        List<ISceneData> scenes = new List<ISceneData>();
        var scenesViewController = controller.scenesViewController;

        yield return new WaitForSeconds(TIME);
        Debug.Log("ADD PROJECT");

        scenes.Add(new SceneData()
        {
            id = "MyProject1",
            isDeployed = false,
            name = "MyProject1"
        });
        scenesViewController.SetScenes(scenes);

        yield return new WaitForSeconds(TIME);
        Debug.Log("ADD DEPLOY");

        scenes.Add(new SceneData()
        {
            id = "MyDeploy1",
            isDeployed = true,
            name = "MyDeploy1"
        });
        scenesViewController.SetScenes(scenes);

        yield return new WaitForSeconds(TIME);
        Debug.Log("ADD DEPLOY2");

        scenes.Add(new SceneData()
        {
            id = "MyDeploy2",
            isDeployed = true,
            name = "MyDeploy2"
        });
        scenesViewController.SetScenes(scenes);

        yield return new WaitForSeconds(TIME);
        Debug.Log("ADD DEPLOY3");

        scenes.Add(new SceneData()
        {
            id = "MyDeploy3",
            isDeployed = true,
            name = "MyDeploy3"
        });
        scenesViewController.SetScenes(scenes);

        yield return new WaitForSeconds(TIME);
        Debug.Log("ADD DEPLOY4");

        scenes.Add(new SceneData()
        {
            id = "MyDeploy4",
            isDeployed = true,
            name = "MyDeploy4"
        });
        scenesViewController.SetScenes(scenes);

        yield return new WaitForSeconds(TIME);
        Debug.Log("REMOVE DEPLOY3");

        scenes = scenes.FindAll((data) => data.id != "MyDeploy3");
        scenesViewController.SetScenes(scenes);

        yield return new WaitForSeconds(TIME);
        Debug.Log("REMOVE DEPLOY4");

        scenes = scenes.FindAll((data) => data.id != "MyDeploy4");
        scenesViewController.SetScenes(scenes);

        yield return new WaitForSeconds(TIME);
        Debug.Log("REMOVE DEPLOY2");

        scenes = scenes.FindAll((data) => data.id != "MyDeploy2");
        scenesViewController.SetScenes(scenes);

        yield return new WaitForSeconds(TIME);
        Debug.Log("REMOVE DEPLOY1");

        scenes = scenes.FindAll((data) => data.id != "MyDeploy1");
        scenesViewController.SetScenes(scenes);

        yield return new WaitForSeconds(TIME);
        Debug.Log("REMOVE ALL");
        scenesViewController.SetScenes(new List<ISceneData>());
    }
}