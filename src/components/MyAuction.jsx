
import { useEffect, useState } from "react";

function MyAuction() {

  const [cars, setCars] = useState([]);
  const [auctions, setAuctions] = useState([]);
  const [titleText, setTitleText] = useState("")
  const [filteredItems, setFilteredItems] = useState([])
  const [bidText, setBidText] = useState("")
  const [auctionIdText, setAuctionIdText] = useState("")
  const [maxBidText, setMaxBidText] = useState("")
  const [currDateText, setCurrDateText] = useState("")

  useEffect(() => {

    async function load() {

      const responseCars = await fetch('/api/cars')
      const cars = await responseCars.json()
      setCars(cars)

      const responseAuctions = await fetch('/api/auctions')
      let auctions = await responseAuctions.json()

      auctions = auctions.sort(compareAuctionsByEndTime)
      console.log(auctions)
      setAuctions(auctions)

    }
    load()
  }, []) // tom array som andra argument = körs endast vid komponentladdning

  function getSelectedCar(id) {

    let listItem;

    for (let i = 0; i < cars.length; i++) {
      if (cars[i].id == id) {
        listItem = cars[i];
      }
    }

    return listItem;

  }

  function getAuctionIndexFromId(id, filteredItems) {

    let index = -1
    let i = 0

    for (i = 0; i < filteredItems.length; i++) {
      if (filteredItems[i].id == id) {
        index = i
      }
    }

    return index

  }

  function compareAuctionsByEndTime(a, b) {

    let dateA = Date.parse(a.endTime)
    let dateB = Date.parse(b.endTime)

    return dateA - dateB

  }

  async function storeAuctionBid(data, id) {

    await fetch(`/api/auctions/${id}`, {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });

  }

  async function addNewBid(event, filteredItems) {

    event.preventDefault()

    let bFound = false
    let n = filteredItems.length
    let bid = 0
    let id = -1
    id = auctionIdText
    let index = getAuctionIndexFromId(id, filteredItems)
    let indexAuction = getAuctionIndexFromId(id, auctions)
    console.log("The id is: " + id)
    console.log("The index is: " + index)
    console.log("The index of auction is: " + indexAuction)

    bid = bidText

    console.log("The bid is: " + bid)


    if (n <= 0 || id < 0 || index < 0) {
      alert(`Something went wrong or the price is too low!`)
      return bFound
    }

    console.log(filteredItems[index].highestBid)

    if (bid > filteredItems[index].highestBid) {
      filteredItems[index].highestBid = bid
      auctions[indexAuction].highestBid = bid
      bFound = true
    }
    else {
      alert(`The price is too low!`)
    }

    if (bFound) {
      setFilteredItems([...filteredItems])
      setAuctions([...auctions])
      console.log(filteredItems[index])
      await storeAuctionBid(filteredItems[index], id)
      alert(`Auction object with id ${filteredItems[index].id} has now recieved a new highest bid!`)
    }

  }

  function setNewMaxBidText(event) {

    setMaxBidText(event.target.value)

  }

  function setNewCurrDateText(event) {

    setCurrDateText(event.target.value)

  }

  function setNewBidText(event) {

    const newBidText = event.target.value.toLowerCase()
    setBidText(event.target.value)

  }

  function setNewAuctionIdText(event) {

    const newIdText = event.target.value.toLowerCase()
    setAuctionIdText(event.target.value)

  }

  function searchTitle(event) {

    const newTitleText = event.target.value.toLowerCase()
    setTitleText(event.target.value)

    const newAuctionList = auctions.filter(item => item.title.toLowerCase().includes(newTitleText))
    setFilteredItems(newAuctionList)

  }

  function filterMaxBid(event) {

    if (maxBidText <= 0) {
      return
    }

    let newAuctionList1 = auctions.filter(item => item.title.toLowerCase().includes(titleText.toLowerCase()))
    setFilteredItems(newAuctionList1)

    let newAuctionList2 = newAuctionList1.filter(item => item.highestBid <= maxBidText)
    setFilteredItems(newAuctionList2)

  }

  function getDateFromText(strDate) {

    let newStrDate = strDate.substring(0, 10)
    let newDate = new Date(Date.parse(newStrDate))

    const formattedDate = newDate.toLocaleDateString('sv-SW', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit'
    });

    return formattedDate

  }


  function filterCurrDate(event) {

    let newAuctionList1 = auctions.filter(item => item.title.toLowerCase().includes(titleText.toLowerCase()))
    setFilteredItems(newAuctionList1)

    let newAuctionList2 = newAuctionList1.filter(item => getDateFromText(item.endTime) == currDateText)
    setFilteredItems(newAuctionList2)

  }

  return (
    <main>

      <form>

        <label for="exampleFormControlInput1">Auction title of the car to search:</label>
        <input className="flex-sm-fill text-sm-center nav-link" name="titleText"
          value={titleText} onChange={searchTitle} /><br />

        <label for="maxBid">Search for a Max bid less than this value:</label>
        <input className="flex-sm-fill text-sm-center nav-link" name="maxBid"
          type="Number" value={maxBidText} onChange={(event) => setNewMaxBidText(event)} />
        <button type="button" onClick={filterMaxBid} >Set Max Price</button> <br /> <br />

        <label for="maxBid">Date for the auction:</label>
        <input className="flex-sm-fill text-sm-center nav-link" name="currDate"
          type="Date" value={currDateText} onChange={(event) => setNewCurrDateText(event)} />
        <button type="button" onClick={filterCurrDate} >Set Date to Check</button> <br /> <br />

      </form>

      <form onSubmit={(event) => addNewBid(event, filteredItems)} >

        <label for="inputBid">Auction id to bid on:
          <input className="flex-sm-fill text-sm-center nav-link" name="auctionIdText"
            type="Number" value={auctionIdText} onChange={(event) => setNewAuctionIdText(event)} />
        </label>

        <label for="inputBid">Set new bid price on car:
          <input className="flex-sm-fill text-sm-center nav-link" name="bidText"
            type="Number" value={bidText} onChange={(event) => setNewBidText(event)} />
        </label>

        <button type="submit" data-click="AddNewBid">Add New Bid</button>
      </form>

      {auctions.length ? (
        <ul className="container">
          {filteredItems.map((auction) => (
            <li className="item" key={auction.id}>
              <div className=".container-p">
                <span className="auctionid">Auction Id: {auction.id}</span><br />
                <span className="auctiontitle">Auction Title: {auction.title}</span><br />
                <span className="starttime">Start Time: {auction.startTime}</span><br />
                <span className="endtime">End Time: {auction.endTime}</span><br />
                <span className="highestbid">Highest Bid: {auction.highestBid}</span><br />
                <span className="carbrand">Brand for the Car: {getSelectedCar(auction.carId).brand}</span><br />
              </div>
            </li>
          ))}
        </ul>
      ) : (
        <p style={{ marginTop: '2rem' }}>Your list is empty.</p>
      )}
    </main>
  )
}

export default MyAuction
