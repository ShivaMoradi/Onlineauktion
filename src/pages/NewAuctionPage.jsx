
import NewAuction from "../components/NewAuction.jsx";


function NewAuctionPage() {

  return (
    <div className="auctionForm">
      <form onSubmit={NewAuction}>
        <br />
        <h3>New Auction</h3><br />
        <label>Auction Id:</label><br /><input type="number" name="id" placeholder="Auction Id..." /><br />
        <label>Title:</label><br /><input type="text" name="title" placeholder="UserName..." /><br />
        <label>Start Time:</label><br /><input type="datetime-local" name="startTime" placeholder="Start date and time..." /><br />
        <label>End Time:</label><br /><input type="datetime-local" name="endTime" placeholder="End date and time..." /><br />
        <label>Highest Bid:</label><br /><input type="number" name="highestBid" placeholder="New bid..." /><br />
        <label>Car Id:</label><br /><input type="number" name="carId" placeholder="Car Id..." /><br />
        <label>User Id:</label><br /><input type="number" name="userId" placeholder="User Id..." /><br />
        <label>Status:</label><br /><input type="number" name="status" placeholder="Status (0, 1, 2)..." /><br /><br />
        <input type="submit" value="Add Auction" />
      </form>
    </div>
  )

}

export default NewAuctionPage
