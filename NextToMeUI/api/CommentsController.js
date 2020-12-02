import APIController from "./ApiController";

class CommentsController extends APIController {
  constructor() {
    super();
  }

  /**  
    @param {Object} comment_info:  
      @param {String} text - текст комментария
      @param {String} messageId - id Сообщения
  */
  async sendComment(comment_info) {
    const newComment = await this.request(
      "post",
      "/message/comments/send",
      comment_info
    );
    return newComment;
  }

  /**  
    @param {String} messageId - id Сообщения
  */
  async getComments(messageId, skip = 0, take = 2147483647) {
    const comments = await this.request(
      "get",
      `/message/comments/get?messageId=${messageId}&skip=${skip}&take=${take}`
    );
    return comments;
  }
}

export default new CommentsController();
