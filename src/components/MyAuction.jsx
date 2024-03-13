import { useEffect, useState } from "react";

function MyAuction() {
  const [auctions, setAuctions] = useState([]);
  const [searchTitle, setSearchTitle] = useState("");
  const [maxBidText, setMaxBidText] = useState("");
  const [currDateText, setCurrDateText] = useState("");
  const [selectedAuction, setSelectedAuction] = useState("");
  const [bidAmount, setBidAmount] = useState("");
  const [userId, setUserId] = useState("user123"); // Anpassa till din användarhanteringslogik

  useEffect(() => {
    async function loadAuctions() {
      try {
        const response = await fetch("/api/auctions");
        if (!response.ok) throw new Error("Failed to fetch auctions");
        const data = await response.json();
        setAuctions(data);
      } catch (error) {
        console.error("Error loading auctions:", error);
      }
    }
    loadAuctions();
  }, []);

  useEffect(() => {
    // Inget behov av att ändra något här, behåller originalupplägget
  }, [searchTitle, maxBidText, currDateText, auctions, selectedAuction]);

  const handleBidInputChange = (e) => {
    setBidAmount(e.target.value);
  };

  async function placeBid(auctionId, bidAmount) {
    try {
      const auction = auctions.find((auction) => auction.id === auctionId);
      if (!auction || parseFloat(bidAmount) <= parseFloat(auction.highestBid)) {
        throw new Error("Bid amount must be higher than the current highest bid.");
      }

      const response = await fetch(`/api/auctions/${auctionId}`, {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          highestBid: bidAmount,
          userId,
        }),
      });

      if (!response.ok) {
        throw new Error("Failed to place bid");
      }

      alert("Bid placed successfully!");
      loadAuctions(); // Ladda om auktioner efter framgångsrikt bud
    } catch (error) {
      console.error("Error placing bid:", error.message);
      alert(error.message);
    }
  }

  // Funktion för att filtrera auktioner baserat på valda kriterier
  const getFilteredAuctions = () => {
    return auctions.filter(auction => {
      return auction.title.toLowerCase().includes(searchTitle.toLowerCase()) &&
        (!maxBidText || parseFloat(auction.highestBid) <= parseFloat(maxBidText)) &&
        (!currDateText || new Date(auction.endTime).toISOString().split('T')[0] === currDateText);
    });
  };

  return (
    <main>
      <div>
        <input
          type="text"
          value={searchTitle}
          onChange={(e) => setSearchTitle(e.target.value)}
          placeholder="Search by title"
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
          placeholder="Filter by end date"
        />
        <select
          value={selectedAuction}
          onChange={(e) => setSelectedAuction(e.target.value)}
        >
          <option value="">-- Select Auction --</option>
          {getFilteredAuctions().map((auction) => (
            <option key={auction.id} value={auction.id}>
              {auction.title}
            </option>
          ))}
        </select>
      </div>
      <ul className="container">
        {getFilteredAuctions().map((auction) => (
          <li key={auction.id} className="item">
            <div className="auction-details">
              <h3>{auction.title}</h3>
              <p>Auction ID: {auction.id}</p>
              <p>Start Time: {auction.startTime}</p>
              <p>End Time: {auction.endTime}</p>
              <p>Highest Bid: ${auction.highestBid}</p>
              {selectedAuction === auction.id && (
                <div>
                  <input
                    type="number"
                    value={bidAmount}
                    onChange={handleBidInputChange}
                    placeholder="Enter bid amount"
                  />
                  <button onClick={() => placeBid(auction.id, parseFloat(bidAmount))}>
                    Place Bid
                  </button>
                </div>
              )}
            </div>
          </li>
        ))}
      </ul>
    </main>
  );
}

export default MyAuction;
