import APIController from "./ApiController";

class CommentsController extends APIController {
  constructor() {
    super();
  }

  /* 
    comment_info: 
      text: string
      messageId: "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  */
  async sendComment(comment_info) {
    const newComment = this.request(
      "send",
      "/message/comments/send",
      comment_info
    );
    return newComment;
  }

  async getComments(messageId, skip = 0, take = 2147483647) {
    const comments = this.request(
      "get",
      `/message/comments/get?messageId=${messageId}&skip=${skip}&take=${take}`
    );
    return comments;
  }
}

export default new CommentsController();
