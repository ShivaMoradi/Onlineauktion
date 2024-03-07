
function NewAuction(event) {

  event.preventDefault()

  const data = new FormData(event.target)
  const info = Object.fromEntries(data)

  fetch("/api/auctions", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(info),
  });

}

export default NewAuction
