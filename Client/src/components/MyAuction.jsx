import { useEffect, useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext";

function MyAuction() {
  const { user } = useContext(AuthContext);

  const [auctions, setAuctions] = useState([]);
  const [searchTitle, setSearchTitle] = useState("");
  const [maxBidText, setMaxBidText] = useState("");
  const [currDateText, setCurrDateText] = useState("");
  const [filteredItems, setFilteredItems] = useState([]);
  const [selectedAuction, setSelectedAuction] = useState(null);
  const [bidAmount, setBidAmount] = useState("");
  const [userId, setUserId] = useState("user123");

  useEffect(() => {
    async function loadAuctions() {
      try {
        const response = await fetch("/api/auctions");
        if (!response.ok) throw new Error("Failed to fetch auctions");
        const data = await response.json();
        setAuctions(data);
        applyFilters(data);
      } catch (error) {
        console.error("Error loading auctions:", error);
      }
    }
    loadAuctions();
  }, []);

  const applyFilters = (auctions) => {
    let result = auctions.filter((auction) =>
      auction.title.toLowerCase().includes(searchTitle.toLowerCase())
    );
    if (maxBidText) {
      result = result.filter(
        (auction) => parseFloat(auction.highestBid) <= parseFloat(maxBidText)
      );
    }
    if (currDateText) {
      const targetDate = new Date(currDateText).toISOString().split("T")[0];
      result = result.filter(
        (auction) =>
          new Date(auction.endTime).toISOString().split("T")[0] === targetDate
      );
    }
    setFilteredItems(result);
  };

  useEffect(() => {
    applyFilters(auctions);
  }, [searchTitle, maxBidText, currDateText, auctions]);

  const handleBidInputChange = (e) => {
    setBidAmount(e.target.value);
  };

  async function storeAuctionBid(data, id) {
    await fetch(`/api/auctions/fromid/${id}`, {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
  }

  function getAuctionIndexFromId(id, items) {
    return items.findIndex((auction) => auction.id === id);
  }

  async function placeBid(auctionId, bidAmount) {
    try {
      const auction = filteredItems.find((auction) => auction.id == auctionId);
      if (!auction) throw new Error("Auction not found.");

      if (!bidAmount) {
        throw new Error("Bid amount must be set to a value");
      }

      if (parseInt(bidAmount) <= parseInt(auction.highestBid)) {
        throw new Error("Bid amount must be higher than the highest bid");
      }

      const newBid = {
        auctionId: parseInt(auctionId),
        bidAmount: parseFloat(bidAmount),
        userId: parseInt(user.id), // Anta att userId är korrekt hanterat och finns
      };

      const response = await fetch(`/api/bids`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newBid),
      });

      if (!response.ok) {
        throw new Error("Failed to place bid");
      }

      alert("Bid placed successfully!");

      const indexFiltered = getAuctionIndexFromId(auctionId, filteredItems);
      filteredItems[indexFiltered].highestBid = bidAmount.toString();

      setFilteredItems([...filteredItems]);
      setAuctions([...auctions]);

      await storeAuctionBid(filteredItems[indexFiltered], auctionId);
    } catch (error) {
      alert(error.message);
    }
  }

  return (
    <main>
      <div>
        <input
          type="text"
          value={searchTitle}
          onChange={(e) => setSearchTitle(e.target.value)}
          placeholder="Search by title"
          data-test="search-auction"
        />
        <input
          type="number"
          value={maxBidText}
          onChange={(e) => setMaxBidText(e.target.value)}
          placeholder="Max bid"
        />
        <input
          type="date"
          value={currDateText}
          onChange={(e) => setCurrDateText(e.target.value)}
        />
      </div>
      <div>
        <select
          data-test="selectedAuction"
          value={selectedAuction || ""}
          onChange={(e) =>
            setSelectedAuction(parseInt(e.target.value, 10) || null)
          }
        >
          <option value="">-- Select Auction --</option>
          {filteredItems.map((auction) => (
            <option key={auction.id} value={auction.id}>
              {auction.title}
            </option>
          ))}
        </select>
        {selectedAuction && (
          <button onClick={() => setSelectedAuction(null)}>
            Back to Auctions
          </button>
        )}
      </div>
      <div>
        {selectedAuction ? (
          <div className="auction-details">
            {filteredItems.find((auction) => auction.id === selectedAuction) ? (
              <>
                <h3 id="auction-title">
                  {
                    filteredItems.find(
                      (auction) => auction.id === selectedAuction
                    ).title
                  }
                </h3>
                <p>Auction ID: {selectedAuction}</p>
                <p>
                  Start Time:{" "}
                  {
                    filteredItems.find(
                      (auction) => auction.id === selectedAuction
                    ).startTime
                  }
                </p>
                <p>
                  End Time:{" "}
                  {
                    filteredItems.find(
                      (auction) => auction.id === selectedAuction
                    ).endTime
                  }
                </p>
                <p
                  data-test="highestbid"
                  value={
                    filteredItems.find(
                      (auction) => auction.id === selectedAuction
                    ).highestBid
                  }
                >
                  Highest Bid: $
                  {
                    filteredItems.find(
                      (auction) => auction.id === selectedAuction
                    ).highestBid
                  }
                </p>
                {user && (
                  <div>
                    <input
                      data-test="bidamount"
                      type="number"
                      value={bidAmount}
                      onChange={handleBidInputChange}
                      placeholder="Enter bid amount"
                    />
                    <button
                      onClick={() =>
                        placeBid(selectedAuction, parseFloat(bidAmount))
                      }
                      data-test="buttonbid"
                    >
                      Place Bid
                    </button>
                  </div>
                )}
              </>
            ) : (
              <p>No auction details available.</p>
            )}
          </div>
        ) : (
          <ul className="container">
            {filteredItems.map((auction) => (
              <li key={auction.id} className="item">
                <div className="auction-details">
                  <h3>{auction.title}</h3>
                  <p>Auction ID: {auction.id}</p>
                  <p>Start Time: {auction.startTime}</p>
                  <p>End Time: {auction.endTime}</p>
                  <p>Highest Bid: ${auction.highestBid}</p>
                </div>
              </li>
            ))}
          </ul>
        )}
      </div>
    </main>
  );
}

export default MyAuction;
