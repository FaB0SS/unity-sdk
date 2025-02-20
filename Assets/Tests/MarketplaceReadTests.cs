using System.Collections;
using System.Numerics;
using NUnit.Framework;
using Thirdweb;
using UnityEngine;
using UnityEngine.TestTools;

public class MarketplaceReadTests : ConfigManager
{
    private GameObject _go;
    private string _marketplaceAddress = "0xc9671F631E8313D53ec0b5358e1a499c574fCe6A";

    [SetUp]
    public void SetUp()
    {
        var existingManager = GameObject.FindObjectOfType<ThirdwebManager>();
        if (existingManager != null)
            GameObject.DestroyImmediate(existingManager.gameObject);

        _go = new GameObject("ThirdwebManager");
        _go.AddComponent<ThirdwebManager>();

        ThirdwebManager.Instance.clientId = GetClientId();
        ThirdwebManager.Instance.Initialize("arbitrum-sepolia");
    }

    [TearDown]
    public void TearDown()
    {
        if (_go != null)
        {
            GameObject.DestroyImmediate(_go);
            _go = null;
        }
    }

    [UnityTest]
    public IEnumerator GetContract_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        Assert.IsNotNull(contract);
        Assert.AreEqual(_marketplaceAddress, contract.Address);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DirectListings_GetAll_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.DirectListings.GetAll();
        yield return new WaitUntil(() => result.IsCompleted);
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        Assert.Greater(result.Result.Count, 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DirectListings_GetAllValid_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.DirectListings.GetAllValid();
        yield return new WaitUntil(() => result.IsCompleted);
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        Assert.GreaterOrEqual(result.Result.Count, 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DirectListings_GetListing_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.DirectListings.GetListing("1");
        yield return new WaitUntil(() => result.IsCompleted);
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DirectListings_GetTotalCount_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.DirectListings.GetTotalCount();
        yield return new WaitUntil(() => result.IsCompleted);
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        Assert.Greater(int.Parse(result.Result), 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DirectListings_IsBuyerApprovedForListing_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.DirectListings.IsBuyerApprovedForListing("1", _marketplaceAddress);
        yield return new WaitUntil(() => result.IsCompleted);
        if (Utils.IsWebGLBuild())
        {
            Assert.IsTrue(result.IsFaulted);
        }
        else
        {
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Result);
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator DirectListings_IsCurrencyApprovedForListing_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.DirectListings.IsCurrencyApprovedForListing("1", _marketplaceAddress);
        yield return new WaitUntil(() => result.IsCompleted);
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnglishAuctions_GetAll_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.EnglishAuctions.GetAll();
        yield return new WaitUntil(() => result.IsCompleted);
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        Assert.Greater(result.Result.Count, 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnglishAuctions_GetAllValid_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.EnglishAuctions.GetAllValid();
        yield return new WaitUntil(() => result.IsCompleted);
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        Assert.GreaterOrEqual(result.Result.Count, 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnglishAuctions_GetAuction_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.EnglishAuctions.GetAuction("0");
        yield return new WaitUntil(() => result.IsCompleted);
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnglishAuctions_GetBidBufferBps_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.EnglishAuctions.GetBidBufferBps("0");
        yield return new WaitUntil(() => result.IsCompleted);
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnglishAuctions_GetMinimumNextBid_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.EnglishAuctions.GetMinimumNextBid("0");
        yield return new WaitUntil(() => result.IsCompleted);
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnglishAuctions_GetTotalCount_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.EnglishAuctions.GetTotalCount();
        yield return new WaitUntil(() => result.IsCompleted);
        if (result.IsFaulted)
            throw result.Exception;
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        Assert.Greater(int.Parse(result.Result), 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnglishAuctions_GetWinner_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.EnglishAuctions.GetWinner("0");
        yield return new WaitUntil(() => result.IsCompleted);
        if (Utils.IsWebGLBuild())
        {
            Assert.IsTrue(result.IsFaulted);
        }
        else
        {
            if (result.IsFaulted)
                throw result.Exception;
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsNotNull(result.Result);
        }
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnglishAuctions_GetWinningBid_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.EnglishAuctions.GetWinningBid("0");
        yield return new WaitUntil(() => result.IsCompleted);
        if (result.IsFaulted)
            throw result.Exception;
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        yield return null;
    }

    [UnityTest]
    public IEnumerator EnglishAuctions_IsWinningBid_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.EnglishAuctions.IsWinningBid("0", "1");
        yield return new WaitUntil(() => result.IsCompleted);
        if (result.IsFaulted)
            throw result.Exception;
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Offers_GetAll_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.Offers.GetAll();
        yield return new WaitUntil(() => result.IsCompleted);
        if (result.IsFaulted)
            throw result.Exception;
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        Assert.Greater(result.Result.Count, 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Offers_GetAllValid_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.Offers.GetAllValid();
        yield return new WaitUntil(() => result.IsCompleted);
        if (result.IsFaulted)
            throw result.Exception;
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        Assert.GreaterOrEqual(result.Result.Count, 0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Offers_GetOffer_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.Offers.GetOffer("0");
        yield return new WaitUntil(() => result.IsCompleted);
        if (result.IsFaulted)
            throw result.Exception;
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Offers_GetTotalCount_Success()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(_marketplaceAddress);
        var result = contract.Marketplace.Offers.GetTotalCount();
        yield return new WaitUntil(() => result.IsCompleted);
        if (result.IsFaulted)
            throw result.Exception;
        Assert.IsTrue(result.IsCompletedSuccessfully);
        Assert.IsNotNull(result.Result);
        Assert.Greater(int.Parse(result.Result), 0);
        yield return null;
    }
}
